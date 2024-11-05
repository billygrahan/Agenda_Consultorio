using System;
using System.Globalization;

namespace Agenda_Consultorio
{
    public class Agendamento
    {
        public string CPF { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public TimeSpan HoraInicial { get; private set; }
        public TimeSpan HoraFinal { get; private set; }

        
        public Agendamento(List<string> CPFs, List<Agendamento> agendamentosExistentes)
        {
            string cpf = Validar_Cpf(CPFs);
            DateTime dataConsulta;
            TimeSpan horaInicial;
            TimeSpan horaFinal;

            do {
                dataConsulta = ObterDataConsulta();
                horaInicial = ObterHoraInicial();
                horaFinal = ObterHoraFinal();
            } while (VerificarDataTimeValido(agendamentosExistentes));


            CPF = cpf;
            DataConsulta = dataConsulta;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }
            
                
        private string Validar_Cpf(List<string> CPFs)
        {
            string cpf;
            do
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
                if (!CPFs.Contains(cpf))
                {
                    Console.WriteLine("Erro: paciente não cadastrado");
                }
            } while (!CPFs.Contains(cpf));

            return cpf;
        }

        private DateTime ObterDataConsulta()
        {
            DateTime dataConsulta;
            while (true)
            {
                Console.Write("Data da Consulta: ");
                string dataConsulta_str = Console.ReadLine();

                if (DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta))
                {
                    if (dataConsulta < DateTime.Now.Date)
                    {
                        Console.WriteLine("\nErro: A data da consulta deve ser para um período futuro.\n");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\nErro: Data no formato incorreto.\n");
                }
            }

            return dataConsulta;
        }

        private TimeSpan ObterHoraInicial()
        {
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

            return horaInicial;
        }

        private TimeSpan ObterHoraFinal()
        {
            TimeSpan horaFinal;
            while (true)
            {
                Console.Write("Hora Final: ");
                string horaFinal_str = Console.ReadLine();

                if (TimeSpan.TryParseExact(horaFinal_str, "hhmm", null, out horaFinal))
                {
                    if (horaFinal <= HoraInicial)
                    {
                        Console.WriteLine("\nErro: A hora final deve ser maior que a hora inicial.\n");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\nErro: Hora final inválida. Use o formato HHMM.\n");
                }
            }

            return horaFinal;
        }

        private bool VerificarDataTimeValido(List<Agendamento> agendamentosExistentes)
        {
            
            if (HoraInicial < new TimeSpan(8, 0, 0) || HoraFinal > new TimeSpan(19, 0, 0))
            {
                Console.WriteLine("\nErro: Horário fora do expediente (8:00 às 19:00).\n");
                return false;
            }

            // Verifica se os horários estão definidos em intervalos de 15 minutos
            if (HoraInicial.Minutes % 15 != 0 || HoraFinal.Minutes % 15 != 0)
            {
                Console.WriteLine("\nErro : Horas devem ser múltiplos de 15 minutos (e.g., 1400, 1415, 1430, etc.).\n");
                return false;
            }

            if (
                agendamentosExistentes.Any(agendamento => agendamento.DataConsulta == DataConsulta &&
                !(HoraFinal <= agendamento.HoraInicial || HoraInicial >= agendamento.HoraFinal))
                )
            {
                Console.WriteLine("\nErro: Horário sobreposto com outro agendamento.\n");
                return false;
            }

            return true;
        }

        public void ImprimirDados()
        {
            Console.WriteLine("\n=================================================================================\n");

            Console.WriteLine($"CPF: {CPF}");
            Console.WriteLine($"Dia: {DataConsulta.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Inicio: {HoraInicial.ToString("hh:mm")}");
            Console.WriteLine($"Fim: {HoraFinal.ToString("hh:mm")}");

            Console.WriteLine("\n=================================================================================\n");
        }

    }
}