
using Agenda_Consultorio.Models;

namespace Agenda_Consultorio.Views;

public class Respostas
{
    public static void ListagemAgenda(List<Agendamento> agendamentos, List<Paciente> pacientes, DateTime? dataInicial = null, DateTime? dataFinal = null)
    {
        var agendamentosFiltrados = agendamentos
            .Where(a => (!dataInicial.HasValue || a.DataConsulta >= dataInicial.Value) &&
                        (!dataFinal.HasValue || a.DataConsulta <= dataFinal.Value))
            .OrderBy(a => a.DataConsulta)
            .ThenBy(a => a.HoraInicial)
            .ToList();

        Console.WriteLine("\n");
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("Data         H.Ini  H.Fim  Tempo   Nome                        Dt.Nasc.");
        Console.WriteLine("-----------------------------------------------------------------------");

        
        DateTime? dataAtual = null;
        foreach (var agendamento in agendamentosFiltrados)
        {
            var paciente = pacientes.Find(pac => pac.CPF == agendamento.CPF);

            if (agendamento.DataConsulta != dataAtual)
            {
                Console.Write(agendamento.DataConsulta.ToString("dd/MM/yyyy "));
                dataAtual = agendamento.DataConsulta;
            }
            else Console.Write("          ");
            
            Console.WriteLine($"{agendamento.HoraInicial:hh\\:mm}   " +
                              $"{agendamento.HoraFinal:hh\\:mm}   " +
                              $"{agendamento.HoraFinal - agendamento.HoraInicial:hh\\:mm}   " +
                              $"{paciente.Nome,-30} " +
                              $"{paciente.DataNascimento:dd/MM/yyyy}");
        }

        Console.WriteLine("------------------------------------------------------------");
    }

    public static void ListagemPacientes(List<Paciente> Pacientes, List<Agendamento> agendamentos, int ordem = 0)
    {
        var pacientes = Pacientes;

        //ordena por cpf
        if (ordem == 1) pacientes = Pacientes.OrderBy(p => p.CPF).ToList();
        
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("CPF          Nome                           Dt.Nasc. Idade");
        Console.WriteLine("-----------------------------------------------------------");

        foreach (var paciente in pacientes)
        {
            int idade = DateTime.Now.Year - paciente.DataNascimento.Year;
            if (paciente.DataNascimento > DateTime.Now.AddYears(-idade)) idade--;

            Console.WriteLine($"{paciente.CPF} {paciente.Nome,-30} {paciente.DataNascimento:dd/MM/yyyy} {idade,3}");

            var consultasPaciente = agendamentos
                .Where(a => a.CPF == paciente.CPF && 
                 (a.DataConsulta > DateTime.Now.Date || 
                 ( a.DataConsulta > DateTime.Now.Date && a.HoraInicial > DateTime.Now.TimeOfDay)))
                .OrderBy(a => a.DataConsulta)
                .ThenBy(a => a.HoraInicial)
                .ToList();

            foreach (var consulta in consultasPaciente)
            {
                Console.WriteLine($"   Agendado para: {consulta.DataConsulta:dd/MM/yyyy}");
                Console.WriteLine($"   {consulta.HoraInicial:hh\\:mm} às {consulta.HoraFinal:hh\\:mm}");
            }
        }

        Console.WriteLine("-----------------------------------------------------------");
    }
}
