using System;
using System.Globalization;
using Agenda_Consultorio.Validations;

namespace Agenda_Consultorio.Models
{
    public class Agendamento
    {
        public string CPF { get; private set; }
        public DateTime DataConsulta { get; private set; }
        public TimeSpan HoraInicial { get; private set; }
        public TimeSpan HoraFinal { get; private set; }


        public Agendamento(List<string> CPFs, List<Agendamento> agendamentosExistentes)
        {
            string cpf = SolicitarCPF(CPFs);
            DateTime dataConsulta;
            TimeSpan horaInicial;
            TimeSpan horaFinal;

            do
            {
                dataConsulta = SolicitarDataConsulta();
                horaInicial = SolicitarHoraInicial();
                horaFinal = SolicitarHoraFinal();
            } while (!ValidationsAgendamento.VerificarDataTimeValido(agendamentosExistentes,dataConsulta,horaInicial,horaFinal));


            CPF = cpf;
            DataConsulta = dataConsulta;
            HoraInicial = horaInicial;
            HoraFinal = horaFinal;
        }


        private string SolicitarCPF(List<string> CPFs)
        {
            string cpf;
            do
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
            } while (!ValidationsAgendamento.ValidaCPF(cpf,CPFs));

            return cpf;
        }

        private DateTime SolicitarDataConsulta()
        {
            DateTime dataConsulta;
            string dataConsulta_str;
            do
            {
                Console.Write("Data da Consulta: ");
                dataConsulta_str = Console.ReadLine();
            } while (!ValidationsAgendamento.ValidaDataConsulta(dataConsulta_str));

            DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta);

            return dataConsulta;
        }

        private TimeSpan SolicitarHoraInicial()
        {
            TimeSpan horaInicial;
            string horaInicial_str;
            do
            {
                Console.Write("Hora Inicial: ");
                horaInicial_str = Console.ReadLine();
            } while (!ValidationsAgendamento.ValidaHoraInicial(horaInicial_str));

            TimeSpan.TryParseExact(horaInicial_str, "hhmm", null, out horaInicial);
            return horaInicial;
        }

        private TimeSpan SolicitarHoraFinal()
        {
            TimeSpan horaFinal;
            string horaFinal_str;

            do
            {
                Console.Write("Hora Final: ");
                horaFinal_str = Console.ReadLine();
            } while (!ValidationsAgendamento.ValidaHoraFinal(horaFinal_str));

            TimeSpan.TryParseExact(horaFinal_str, "hhmm", null, out horaFinal);
            return horaFinal;
        }

    }
}