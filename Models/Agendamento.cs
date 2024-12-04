using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Agenda_Consultorio.Validations;
using Agenda_Consultorio.Views;

namespace Agenda_Consultorio.Models;

[Table("Agendamentos")]
public class Agendamento
{
    [Key]
    public int Id { get; private set; }

    [Required]
    public string CPF { get; private set; } = null!;

    [ForeignKey("CPF")]
    public Paciente Paciente { get; private set; } = null!;

    [Required]
    public DateTime DataConsulta { get; private set; }

    [Required]
    public TimeSpan HoraInicial { get; private set; }

    [Required]
    public TimeSpan HoraFinal { get; private set; }

    public Agendamento() { }

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
        } while (!ValidationsAgendamento.VerificarDataTimeValido(agendamentosExistentes, dataConsulta, horaInicial, horaFinal));


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
            cpf = Menus.ObterRespostagenerica("CPF: ");
        } while (!ValidationsAgendamento.ValidaCPF(cpf, CPFs));

        return cpf;
    }

    private DateTime SolicitarDataConsulta()
    {
        DateTime dataConsulta;
        string dataConsulta_str;
        do
        {
            dataConsulta_str = Menus.ObterRespostagenerica("Data da Consulta: ");
        } while (!ValidationsAgendamento.ValidaDataConsulta(dataConsulta_str));

        DateTime.TryParseExact(dataConsulta_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConsulta);

        return dataConsulta.ToUniversalTime();
    }

    private TimeSpan SolicitarHoraInicial()
    {
        TimeSpan horaInicial;
        string horaInicial_str;
        do
        {
            horaInicial_str = Menus.ObterRespostagenerica("Hora Inicial: ");
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
            horaFinal_str = Menus.ObterRespostagenerica("Hora Final: ");
        } while (!ValidationsAgendamento.ValidaHoraFinal(horaFinal_str));

        TimeSpan.TryParseExact(horaFinal_str, "hhmm", null, out horaFinal);
        return horaFinal;
    }
}
