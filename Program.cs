using Agenda_Consultorio;
using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
using System.Globalization;

Sistema sistema = new Sistema();

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
                if (!sistema.CadastrarPaciente(new Paciente(sistema.CPFsPacientes)))
                    Console.WriteLine("\nErro: paciente não cadastrado\n");
                else Console.WriteLine("\nPaciente cadastrado com sucesso!\n");
            }

            //excluir paciente
            else if (comando2 == 2)
            {
                Console.Write("CPF:");
                string cpf = Console.ReadLine();

                int exclusao = sistema.ExcluirPaciente(cpf);

                if (exclusao == 1) Console.WriteLine("\nPaciente excluído com sucesso!\n");
                else if (exclusao == 2) Console.WriteLine("\nErro: paciente não cadastrado\n");
                else Console.WriteLine("\nErro: paciente está agendado.\n");
            }

            //listar pacientes por CPF
            else if (comando2 == 3) Respostas.ListagemPacientes(sistema.Pacientes, sistema.Agendamentos, 1);

            //listar pacientes por nome
            else if (comando2 == 4) Respostas.ListagemPacientes(sistema.Pacientes, sistema.Agendamentos);

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
                if (sistema.CadastrarConsulta(new Agendamento(sistema.CPFsPacientes, sistema.Agendamentos)))
                    Console.WriteLine("\nAgendamento realizado com sucesso!\n");
                else Console.WriteLine("\nErro: Paciente ja tem uma consulta marcada\n");
            }

            //Cancelar Agendamento
            else if (comando3 == 2)
            {
                string cpf;
                do
                {
                    Console.Write("CPF: ");
                    cpf = Console.ReadLine();
                    if (!sistema.CPFsPacientes.Contains(cpf))
                        Console.WriteLine("\nErro: paciente não cadastrado\n");
                } while (!sistema.CPFsPacientes.Contains(cpf));

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

                if (sistema.ExcluirAgendamento(cpf, dataConsulta, horaInicial))
                    Console.WriteLine("\nAgendamento cancelado com sucesso!\n");
                else Console.WriteLine("\nErro: agendamento não encontrado\n");
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

                    Respostas.ListagemAgenda(sistema.Agendamentos, sistema.Pacientes, dataInicial, dataFinal);
                }

                else if (apresentar == 'T')
                    Respostas.ListagemAgenda(sistema.Agendamentos, sistema.Pacientes);
            }

            else if (comando3 == 4) 
                break;

            else Console.WriteLine("\nErro: Comando não reconhecido\n");
        }
    
    //sair
    else if (comando1 == 3) break;

    else Console.WriteLine("\nErro: comando não reconhecido!\n");
}