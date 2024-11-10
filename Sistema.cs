using Agenda_Consultorio.Models;

namespace Agenda_Consultorio;

public class Sistema
{
    // Alteradas para listas mutáveis
    public List<Paciente> Pacientes { get; private set; } = new List<Paciente>();
    public List<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();
    public List<string> CPFsPacientes { get; private set; } = new List<string>();

    public bool CadastrarPaciente(Paciente novoPaciente)
    {
        if (novoPaciente != null)
        {
            Pacientes.Add(novoPaciente);
            CPFsPacientes.Add(novoPaciente.CPF);
            Pacientes = Pacientes.OrderBy(p => p.Nome).ToList();
            return true;
        }
        else
            return false;
    }

    public bool CadastrarConsulta(Agendamento novoAgendamento)
    {
        var consulta_pendente = Agendamentos.Find
            (
                agend => agend.CPF == novoAgendamento.CPF && 
                agend.DataConsulta >= DateTime.Now.Date && 
                novoAgendamento.HoraFinal >= DateTime.Now.TimeOfDay
            );

        if ( consulta_pendente != null)
        {
            return false;
        }

        else if (novoAgendamento != null)
        {
            Agendamentos.Add(novoAgendamento);
            Agendamentos = Agendamentos
                    .OrderBy(a => a.DataConsulta)
                    .ThenBy(a => a.HoraInicial)
                    .ToList();
            return true;
        }
        else
            return false ;
    }

    public int ExcluirPaciente(string CPF)
    {
        var paciente = Pacientes.Find(Lpac => Lpac.CPF == CPF);

        if (paciente == null)
            return 2;

        var AgendamentoFuturo = Agendamentos.Find
                (
                    Agenda => Agenda.CPF == CPF && 
                    (Agenda.DataConsulta > DateTime.Now.Date || 
                    (Agenda.DataConsulta == DateTime.Now.Date && Agenda.HoraInicial > DateTime.Now.TimeOfDay))
                );

        if (AgendamentoFuturo != null)
            return 3;
        else
        {
            Pacientes.Remove(paciente);
            CPFsPacientes.Remove(CPF);
            Agendamentos.RemoveAll(a => a.CPF == CPF);
            return 1;
        }
    }

    public bool ExcluirAgendamento(string CPF, DateTime DataConsulta, TimeSpan HoraInicial)
    {
        var agendamento = Agendamentos.Find(agenda =>
                                    agenda.CPF == CPF &&
                                    agenda.DataConsulta == DataConsulta &&
                                    agenda.HoraInicial == HoraInicial
        );


        if (agendamento == null)
            return false;
        
        if (DataConsulta > DateTime.Now.Date || (DataConsulta == DateTime.Now.Date && HoraInicial > DateTime.Now.TimeOfDay))
        {
            Agendamentos.Remove(agendamento);
            return true;
        }
        else return false;
    }

}
