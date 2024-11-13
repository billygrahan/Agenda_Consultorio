using Agenda_Consultorio.Models;

namespace Agenda_Consultorio.Validations;

public class ValidationsSistema
{
    protected bool ValidaCadastroPaciente(Paciente novoPaciente)
    {
        if (novoPaciente != null)
        {
            return true;
        }
        return false;
    }

    protected bool ValidaExclusaoPaciente(string CPF, List<Paciente> Pacientes, List<Agendamento> Agendamentos)
    {
        var paciente = Pacientes.Find(Lpac => Lpac.CPF == CPF);

        if (paciente == null)
        {
            Console.WriteLine("\nErro: paciente não cadastrado\n");
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
            Console.WriteLine("\nErro: paciente está agendado.\n");
            return false;
        }

        return true;
    }

    protected bool ValidaCadastroConsulta(Agendamento novoAgendamento, List<Agendamento> Agendamentos)
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
            Console.WriteLine("\nErro: Paciente já tem uma consulta marcada\n");
            return false;
        }


        if (novoAgendamento == null)
        {
            Console.WriteLine("\nErro: Agendamento é nulo\n");
            return false;
        }
        return true;
    }

    protected bool ValidaExclusaoAgendamento(string CPF, DateTime DataConsulta, TimeSpan HoraInicial, List<Agendamento> Agendamentos)
    {
        var agendamento = Agendamentos.Find(agenda =>
                                    agenda.CPF == CPF &&
                                    agenda.DataConsulta == DataConsulta &&
                                    agenda.HoraInicial == HoraInicial
        );


        if (agendamento == null)
        {
            Console.WriteLine("\nErro: agendamento não encontrado\n");
            return false;
        }

        if (DataConsulta > DateTime.Now.Date || (DataConsulta == DateTime.Now.Date && HoraInicial > DateTime.Now.TimeOfDay))
        {
            return true;
        }
        else
        {
            Console.WriteLine("\nErro: agendamento não encontrado\n");
            return false;
        }
    }
}
