using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
using Agenda_Consultorio.Validations;
using System.Globalization;
using Agenda_Consultorio.Context;
using Agenda_Consultorio.Controllers;

namespace Agenda_Consultorio;

/*
Nota sobre o programa:
Este sistema de agendamento e cadastro utiliza, tambem, listas em memória para manipular os dados de pacientes e agendamentos, carregando-os do banco de dados ao iniciar. 
Embora funcional, isso pode impactar o desempenho e a consistência dos dados em situações com grande volume de informações ou múltiplos usuários simultâneos. 
Ajustes ainda estão sendo feitos no programa para realizar consultas diretamente no banco de dados quando necessário, eliminando a dependência do carregamento inicial e permitindo maior escalabilidade e confiabilidade.
*/

public class Sistema
{
    // Alteradas para listas mutáveis
    private List<Paciente>? Pacientes  = new List<Paciente>();
    private List<Agendamento>? Agendamentos  = new List<Agendamento>();
    private List<string>? CPFsPacientes  = new List<string>();

    private readonly AppDbContext BD;
    private AgendamentosController agendamentosController;
    private PacientesController pacientesController;

    public Sistema()
    {
        BD = new();
        agendamentosController = new AgendamentosController(BD);
        pacientesController = new PacientesController(BD);

        Pacientes = BD.Pacientes.ToList();

        Agendamentos = BD.Agendamentos.ToList();

        CPFsPacientes = Pacientes.Select(p => p.CPF).ToList();
    }
    public bool CadastrarPaciente(Paciente novoPaciente)
    {
        if (ValidationsSistema.ValidaCadastroPaciente(novoPaciente))
        {
            if (pacientesController.PostPaciente(novoPaciente))
            {
                Pacientes.Add(novoPaciente);
                CPFsPacientes.Add(novoPaciente.CPF);
                Pacientes = Pacientes.OrderBy(p => p.Nome).ToList();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }
    public bool ExcluirPaciente(string CPF)
    {
        var paciente = Pacientes.Find(Lpac => Lpac.CPF == CPF);

        if (ValidationsSistema.ValidaExclusaoPaciente(CPF, Pacientes, Agendamentos))
        {
            if (pacientesController.DeletePaciente(paciente))
            {
                Pacientes.Remove(paciente);
                CPFsPacientes.Remove(CPF);
                Agendamentos.RemoveAll(a => a.CPF == CPF);
                return true;
            }
            else { return false; }
        }
        else return false;
    }

    public bool CadastrarConsulta(Agendamento novoAgendamento)
    {
        if (ValidationsSistema.ValidaCadastroConsulta(novoAgendamento, Agendamentos))
        {
            if (agendamentosController.PostAgendamento(novoAgendamento))
            {
                Agendamentos.Add(novoAgendamento);
                Agendamentos = Agendamentos
                        .OrderBy(a => a.DataConsulta)
                        .ThenBy(a => a.HoraInicial)
                        .ToList();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false ;
    }

    public bool ExcluirAgendamento(string CPF, DateTime DataConsulta, TimeSpan HoraInicial)
    {
        var agendamento = Agendamentos.Find(agenda =>
                                    agenda.CPF == CPF &&
                                    agenda.DataConsulta == DataConsulta &&
                                    agenda.HoraInicial == HoraInicial
        );
        
        if (ValidationsSistema.ValidaExclusaoAgendamento(CPF, DataConsulta, HoraInicial, Agendamentos))
        {
            if (agendamentosController.DeleteAgendamento(agendamento))
            {
                Agendamentos.Remove(agendamento);
                return true;
            }
            else
            {
                return false;
            }
        }
        else return false;
    }

    public void run()
    {
        while (true)
        {
            int comando1 = Menus.MenuInicial();

            //menu pacientes
            if (comando1 == 1)
            {
                while (true)
                {
                    int comando2 = Menus.MenuPacientes();

                    //cadastrar paciente
                    if (comando2 == 1)
                    {
                        if (CadastrarPaciente(new Paciente(CPFsPacientes)))
                            Console.WriteLine("\nPaciente cadastrado com sucesso!\n");
                    }

                    //excluir paciente
                    else if (comando2 == 2)
                    {
                        string cpf = Menus.ObterRespostagenerica("CPF: ");

                        if (ExcluirPaciente(cpf)) Console.WriteLine("\nPaciente excluído com sucesso!\n");
                    }

                    //listar pacientes por CPF
                    else if (comando2 == 3) Respostas.ListagemPacientes(Pacientes, Agendamentos, 1);

                    //listar pacientes por nome
                    else if (comando2 == 4) Respostas.ListagemPacientes(Pacientes, Agendamentos);

                    //sair (menu pacientes)
                    else if (comando2 == 5) break;

                    else Errors.MensagemdeErro("Comand-incorrect");
                }
            }

            //menu da agenda
            else if (comando1 == 2)
            {
                while (true)
                {
                    int comando3 = Menus.MenuAgenda();

                    //Agendar consulta
                    if (comando3 == 1)
                    {
                        if (CadastrarConsulta(new Agendamento(CPFsPacientes, Agendamentos)))
                            Console.WriteLine("\nAgendamento realizado com sucesso!\n");
                    }

                    //Cancelar Agendamento
                    else if (comando3 == 2)
                    {
                        string cpf;
                        do
                        {
                            cpf = Menus.ObterRespostagenerica("CPF: ");
                            if (!CPFsPacientes.Contains(cpf))
                                Errors.MensagemdeErro("paciente nao cadastrado");
                        } while (!CPFsPacientes.Contains(cpf));

                        DateTime dataConsulta;
                        while (true)
                        {
                            string dataConsulta_str = Menus.ObterRespostagenerica("Data da Consulta: ");
                            if (DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta)) break;
                            else Errors.MensagemdeErro("DateTime form");
                        }

                        TimeSpan horaInicial;
                        while (true)
                        {
                            string horaInicial_str = Menus.ObterRespostagenerica("Hora Inicial: ");

                            if (TimeSpan.TryParseExact(horaInicial_str, "hhmm", null, out horaInicial))
                            {
                                break;
                            }
                            else
                            {
                                Errors.MensagemdeErro("TimeSpan form");
                            }
                        }

                        if (ExcluirAgendamento(cpf, dataConsulta, horaInicial))
                            Console.WriteLine("\nAgendamento cancelado com sucesso!\n");
                    }

                    //listar agendamentos
                    else if (comando3 == 3)
                    {
                        Console.Write("Apresentar a agenda T-Toda ou P-Periodo:");
                        char apresentar = char.ToUpper(Console.ReadKey().KeyChar);

                        if (apresentar == 'P')
                        {
                            Console.WriteLine("\n");

                            DateTime dataInicial;
                            while (true)
                            {
                                string dataInicial_str = Menus.ObterRespostagenerica("Data inicial: ");
                                if (DateTime.TryParseExact(dataInicial_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataInicial)) break;
                                else Errors.MensagemdeErro("DateTime form");
                            }

                            DateTime dataFinal;
                            while (true)
                            {
                                string dataFinal_str = Menus.ObterRespostagenerica("Data final: ");
                                if (DateTime.TryParseExact(dataFinal_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFinal)) break;
                                else Errors.MensagemdeErro("DateTime form");
                            }

                            Respostas.ListagemAgenda(Agendamentos, Pacientes, dataInicial, dataFinal);
                        }

                        else if (apresentar == 'T')
                            Respostas.ListagemAgenda(Agendamentos, Pacientes);
                    }

                    else if (comando3 == 4)
                        break;

                    else Errors.MensagemdeErro("Comand-incorrect");
                }
            }

            //sair
            else if (comando1 == 3) break;

            else Errors.MensagemdeErro("Comand-incorrect");
        }
    }
}
