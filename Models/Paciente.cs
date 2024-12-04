using System.Globalization;
using Agenda_Consultorio.Validations;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda_Consultorio.Controllers;
using Agenda_Consultorio.Views;

namespace Agenda_Consultorio.Models;

[Table("Pacientes")]
public class Paciente
{
    [Key]
    public string CPF { get; private set; }

    [Required]
    public string Nome { get; private set; }

    [Required]
    public DateTime DataNascimento { get; private set; }

    public ICollection<Agendamento>? Agendamentos { get; private set; }

    // Construtor padrão (necessário para o EF)
    public Paciente()
    {
        Agendamentos = new Collection<Agendamento>();
    }

    // Construtor com argumentos
    public Paciente(List<string> CPFs)
    {
        CPF = SolicitarCPF(CPFs);
        Nome = SolicitarNome();
        DataNascimento = SolicitarDataNascimento();

        Agendamentos = new Collection<Agendamento>();
    }

    private string SolicitarNome()
    {
        string nome = "";

        do
        {
            nome = Menus.ObterRespostagenerica("Nome: ");
        } while (!ValidationsPaciente.ValidaNome(nome));

        return nome;
    }

    private string SolicitarCPF(List<string> CPFs)
    {
        string cpf = "";
        do
        {
            cpf = Menus.ObterRespostagenerica("CPF: ");
        } while (!ValidationsPaciente.ValidaCPF(cpf, CPFs));

        return cpf;
    }

    private DateTime SolicitarDataNascimento()
    {
        DateTime dataNascimento;
        string dataNascimento_str;

        do
        {
            dataNascimento_str = Menus.ObterRespostagenerica("Data de Nascimento: ");
        } while (!ValidationsPaciente.ValidaDataNascimento(dataNascimento_str));

        DateTime.TryParseExact(dataNascimento_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento);
        return dataNascimento.ToUniversalTime();
    }
}


