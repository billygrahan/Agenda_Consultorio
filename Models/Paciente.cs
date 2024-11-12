using System;
using System.Globalization;
using Agenda_Consultorio.Validations;

namespace Agenda_Consultorio.Models;

public class Paciente : ValidationsPaciente
{
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public DateTime DataNascimento { get; private set; }

    public Paciente(List<string> CPFs)
    {
        //Console.WriteLine("\n=================================================================================");
        Nome = SolicitarNome();
        CPF = SolicitarCPF(CPFs);
        DataNascimento = SolicitarDataNascimento();
        //Console.WriteLine("=================================================================================\n");
    }

    private string SolicitarNome()
    {
        string nome = "";

        do
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine();
        } while (!ValidaNome(nome));

        return nome;
    }

    private string SolicitarCPF(List<string> CPFs)
    {
        string cpf = "";
        do
        {
            Console.Write("CPF: ");
            cpf = Console.ReadLine();
        } while (!ValidaCPF(cpf,CPFs));

        return cpf;
    }

    private DateTime SolicitarDataNascimento()
    {
        DateTime dataNascimento;
        string dataNascimento_str;

        do
        {
            Console.Write("Data de Nascimento: ");
            dataNascimento_str = Console.ReadLine();
        } while (!ValidaDataNascimento(dataNascimento_str));

        DateTime.TryParseExact(dataNascimento_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento);
        return dataNascimento;
    }
}

