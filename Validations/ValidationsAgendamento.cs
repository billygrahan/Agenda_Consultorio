using Agenda_Consultorio.Models;
using System.Globalization;

namespace Agenda_Consultorio.Validations;

public class ValidationsAgendamento
{
    public static bool ValidaCPF(string cpf, List<string> CPFs)
    {

        if (!CPFs.Contains(cpf))
        {
            Console.WriteLine("Erro: paciente não cadastrado");
            return false;
        }
        return true;
    }

    public static bool ValidaDataConsulta(string dataConsulta_str)
    {
        DateTime dataConsulta;
        if (DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta))
        {
            if (dataConsulta < DateTime.Now.Date)
            {
                Console.WriteLine("\nErro: A data da consulta deve ser para um período futuro.\n");
                return false;
            }
            return true;
        }
        else
        {
            Console.WriteLine("\nErro: Data no formato incorreto.\n");
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
            Console.WriteLine("\nErro: Hora inicial inválida. Use o formato HHMM. XXXXXXXXXXXXXXXXXXXX\n");
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
        return false;
    }

    public static bool VerificarDataTimeValido(List<Agendamento> agendamentosExistentes, DateTime datConsulta, TimeSpan horaInicio, TimeSpan horaFinal)
    {

            if (horaInicio < new TimeSpan(8, 0, 0) || horaFinal > new TimeSpan(19, 0, 0))
            {
                Console.WriteLine("\nErro: Horário fora do expediente (8:00 às 19:00).\n");
                return false;
            }

            if (horaInicio.Minutes % 15 != 0 || horaFinal.Minutes % 15 != 0)
            {
                Console.WriteLine("\nErro : Horas devem ser múltiplos de 15 minutos (e.g., 1400, 1415, 1430, etc.).\n");
                return false;
            }

            if (
                agendamentosExistentes.Any(agendamento => agendamento.DataConsulta == datConsulta &&
                !(horaFinal <= agendamento.HoraInicial || horaInicio >= agendamento.HoraFinal))
                )
            {
                Console.WriteLine("\nErro: já existe uma consulta agendada nesse horário\n");
                return false;
            }

            if (horaInicio >= horaFinal)
            {
                Console.WriteLine("\nErro: Hora final deve ser após a Hora inicial.\n");
                return false;
            }

            return true;
        }
}
