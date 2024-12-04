using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
using System.Globalization;

namespace Agenda_Consultorio.Validations;

public class ValidationsAgendamento
{
    public static bool ValidaCPF(string cpf, List<string> CPFs)
    {

        if (!CPFs.Contains(cpf))
        {
            Errors.MensagemdeErro("paciente nao cadastrado");
            return false;
        }
        return true;
    }

    public static bool ValidaDataConsulta(string dataConsulta_str)
    {
        DateTime dataConsulta;
        if (DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta))
        {
            if (dataConsulta < DateTime.Now.Date ||(dataConsulta == DateTime.Now.Date && DateTime.Now.TimeOfDay > new TimeSpan(18,45,0)))
            {
                Errors.MensagemdeErro("data no passado");
                return false;
            }
            return true;
        }
        else
        {
            Errors.MensagemdeErro("DateTime form");
            return false;
        }
    }

    public static bool ValidaHoraInicial(string horaInicial_str)
    {
        TimeSpan horaInicial;
        if (TimeSpan.TryParseExact(horaInicial_str, "hhmm", null, out horaInicial))
        {
            return true;
        }
        else
        {
            Errors.MensagemdeErro("TimeSpan form");
            return false;
        }
    }

    public static bool ValidaHoraFinal(string horaFinal_str)
    {
        TimeSpan horaFinal;
        if (TimeSpan.TryParseExact(horaFinal_str, "hhmm", null, out horaFinal))
        {
            return true;
        }
        else
        {
            Errors.MensagemdeErro("TimeSpan form");
            return false;
        }
    }

    public static bool VerificarDataTimeValido(List<Agendamento> agendamentosExistentes, DateTime datConsulta, TimeSpan horaInicio, TimeSpan horaFinal)
    {
            if (horaInicio >= horaFinal)
            {
                Errors.MensagemdeErro("final maior que inicial");
                return false;
            }
            if (horaInicio < new TimeSpan(8, 0, 0) || horaFinal > new TimeSpan(19, 0, 0))
            {
                Errors.MensagemdeErro("expediente");
                return false;
            }

            if (horaInicio.Minutes % 15 != 0 || horaFinal.Minutes % 15 != 0)
            {
                Errors.MensagemdeErro("TimeSpan multiple 15");
                return false;
            }

            if (
                agendamentosExistentes.Any(agendamento => agendamento.DataConsulta == datConsulta &&
                !(horaFinal <= agendamento.HoraInicial || horaInicio >= agendamento.HoraFinal))
                )
            {
                Errors.MensagemdeErro("colisao entre consultas");
                return false;
            }

            return true;
        }
}
