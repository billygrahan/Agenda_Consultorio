using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
using Agenda_Consultorio.Validations;
using System.Globalization;

namespace Agenda_Consultorio;

public class Sistema : ValidationsSistema
{
    // Alteradas para listas mutáveis
    public List<Paciente> Pacientes { get; private set; } = new List<Paciente>();
    public List<Agendamento> Agendamentos { get; private set; } = new List<Agendamento>();
    public List<string> CPFsPacientes { get; private set; } = new List<string>();

    public bool CadastrarPaciente(Paciente novoPaciente)
    {
        if (ValidaCadastroPaciente(novoPaciente))
        {
            Pacientes.Add(novoPaciente);
            CPFsPacientes.Add(novoPaciente.CPF);
            Pacientes = Pacientes.OrderBy(p => p.Nome).ToList();
            return true;
        }
        else
            return false;
    }
    public bool ExcluirPaciente(string CPF)
    {
        var paciente = Pacientes.Find(Lpac => Lpac.CPF == CPF);

        if (!ValidaExclusaoPaciente(CPF,Pacientes,Agendamentos))
            return false;
        else
        {
            Pacientes.Remove(paciente);
            CPFsPacientes.Remove(CPF);
            Agendamentos.RemoveAll(a => a.CPF == CPF);
            return true;
        }
    }

    public bool CadastrarConsulta(Agendamento novoAgendamento)
    {
        if (ValidaCadastroConsulta(novoAgendamento, Agendamentos))
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

    

    public bool ExcluirAgendamento(string CPF, DateTime DataConsulta, TimeSpan HoraInicial)
    {
        var agendamento = Agendamentos.Find(agenda =>
                                    agenda.CPF == CPF &&
                                    agenda.DataConsulta == DataConsulta &&
                                    agenda.HoraInicial == HoraInicial
        );
        
        if (ValidaExclusaoAgendamento(CPF, DataConsulta, HoraInicial, Agendamentos))
        {
            Agendamentos.Remove(agendamento);
            return true;
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
                while (true)
                {
                    int comando2 = Menus.MenuPacientes();

                    //cadastrar paciente
                    if (comando2 == 1)
                    {
                        if (!CadastrarPaciente(new Paciente(CPFsPacientes)))
                            Console.WriteLine("\nErro: paciente não cadastrado\n");
                        else Console.WriteLine("\nPaciente cadastrado com sucesso!\n");
                    }

                    //excluir paciente
                    else if (comando2 == 2)
                    {
                        Console.Write("CPF:");
                        string cpf = Console.ReadLine();

                        if (ExcluirPaciente(cpf)) Console.WriteLine("\nPaciente excluído com sucesso!\n");
                    }

                    //listar pacientes por CPF
                    else if (comando2 == 3) Respostas.ListagemPacientes(Pacientes, Agendamentos, 1);

                    //listar pacientes por nome
                    else if (comando2 == 4) Respostas.ListagemPacientes(Pacientes, Agendamentos);

                    //sair (menu pacientes)
                    else if (comando2 == 5) break;

                    else Console.WriteLine("\nErro:comando não reconhecido!\n");
                }

            //menu da agenda
            else if (comando1 == 2)
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
                            Console.Write("CPF: ");
                            cpf = Console.ReadLine();
                            if (!CPFsPacientes.Contains(cpf))
                                Console.WriteLine("\nErro: paciente não cadastrado\n");
                        } while (!CPFsPacientes.Contains(cpf));

                        DateTime dataConsulta;
                        while (true)
                        {
                            Console.Write("Data da Consulta: ");
                            string dataConsulta_str = Console.ReadLine();
                            if (DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta)) break;
                            else Console.WriteLine("\nErro: Data no formato incorreto.\n");
                        }

                        TimeSpan horaInicial;
                        while (true)
                        {
                            Console.Write("Hora Inicial: ");
                            string horaInicial_str = Console.ReadLine();

                            if (TimeSpan.TryParseExact(horaInicial_str, "hhmm", null, out horaInicial))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nErro: Hora inicial inválida. Use o formato HHMM. XXXXXXXXXXXXXXXXXXXX\n");
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
                                Console.Write("Data inicial: ");
                                string dataInicial_str = Console.ReadLine();
                                if (DateTime.TryParseExact(dataInicial_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataInicial)) break;
                                else Console.WriteLine("\nErro: Data no formato incorreto.\n");
                            }

                            DateTime dataFinal;
                            while (true)
                            {
                                Console.Write("Data final: ");
                                string dataFinal_str = Console.ReadLine();
                                if (DateTime.TryParseExact(dataFinal_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFinal)) break;
                                else Console.WriteLine("\nErro: Data no formato incorreto.\n");
                            }

                            Respostas.ListagemAgenda(Agendamentos, Pacientes, dataInicial, dataFinal);
                        }

                        else if (apresentar == 'T')
                            Respostas.ListagemAgenda(Agendamentos, Pacientes);
                    }

                    else if (comando3 == 4)
                        break;

                    else Console.WriteLine("\nErro: Comando não reconhecido\n");
                }

            //sair
            else if (comando1 == 3) break;

            else Console.WriteLine("\nErro: comando não reconhecido!\n");
        }
    }
}
