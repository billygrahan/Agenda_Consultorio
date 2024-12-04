using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;

namespace Agenda_Consultorio.Validations;

public class ValidationsSistema
{
    public static bool ValidaCadastroPaciente(Paciente novoPaciente)
    {
        if (novoPaciente != null)
        {
            return true;
        }
        return false;
    }

    public static bool ValidaExclusaoPaciente(string CPF, List<Paciente> Pacientes, List<Agendamento> Agendamentos)
    {
        var paciente = Pacientes.Find(Lpac => Lpac.CPF == CPF);

        if (paciente == null)
        {
            Errors.MensagemdeErro("paciente nao cadastrado");
            return false;
        }

        var AgendamentoFuturo = Agendamentos.Find
                (
                    Agenda => Agenda.CPF == CPF &&
                    (Agenda.DataConsulta > DateTime.Now.Date ||
                    (Agenda.DataConsulta == DateTime.Now.Date && Agenda.HoraInicial > DateTime.Now.TimeOfDay))
                );

        if (AgendamentoFuturo != null)
        {
            Errors.MensagemdeErro("paciente agendado");
            return false;
        }

        return true;
    }

    public static bool ValidaCadastroConsulta(Agendamento novoAgendamento, List<Agendamento> Agendamentos)
    {
        var consulta_pendente = Agendamentos.Find
        (
            agend =>
            agend.CPF == novoAgendamento.CPF &&
            agend.DataConsulta > DateTime.Now.Date ||
            (agend.DataConsulta == DateTime.Now.Date && agend.HoraFinal > DateTime.Now.TimeOfDay)
        );

        if (consulta_pendente != null)
        {
            Errors.MensagemdeErro("consulta marcada");
            return false;
        }


        if (novoAgendamento == null)
        {
            Errors.MensagemdeErro("new agendamento is null");
            return false;
        }
        return true;
    }

    public static bool ValidaExclusaoAgendamento(string CPF, DateTime DataConsulta, TimeSpan HoraInicial, List<Agendamento> Agendamentos)
    {
        var agendamento = Agendamentos.Find(agenda =>
                                    agenda.CPF == CPF &&
                                    agenda.DataConsulta == DataConsulta &&
                                    agenda.HoraInicial == HoraInicial
        );


        if (agendamento == null)
        {
            Errors.MensagemdeErro("agendamento nao encontrado");
            return false;
        }

        if (DataConsulta > DateTime.Now.Date || (DataConsulta == DateTime.Now.Date && HoraInicial > DateTime.Now.TimeOfDay))
        {
            return true;
        }
        else
        {
            Errors.MensagemdeErro("agendamento nao encontrado");
            return false;
        }
    }
}
