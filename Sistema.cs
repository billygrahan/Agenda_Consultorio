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
        if (novoAgendamento != null)
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

    /*public bool ExcluirPaciente()
    {

    }

    public bool ExcluirAgendamento()
    {

    }*/
}
