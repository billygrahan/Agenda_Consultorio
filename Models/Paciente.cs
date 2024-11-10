using System;
using System.Globalization;

namespace Agenda_Consultorio.Models;

public class Paciente
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
        string nome;
        do
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine();
            if (nome.Length < 5)
                Console.WriteLine("\nErro: o nome deve ter pelo menos 5 caracteres.\n");
        } while (nome.Length < 5);

        return nome;
    }

    private string SolicitarCPF(List<string> CPFs)
    {
        string cpf;
        do
        {
            Console.Write("CPF: ");
            cpf = Console.ReadLine();
            if (!ValidarCPF(cpf))
                Console.WriteLine("\nErro: CPF inválido\n");
            else if (CPFs.Contains(cpf))
                Console.WriteLine("\nErro: CPF já cadastrado\n");
        } while (!ValidarCPF(cpf) && CPFs.Contains(cpf));

        return cpf;
    }

    private bool VerificaNovoCPF()
    {
        return true;
    }

    private bool Numeros_Iguais(string cpf)
    {
        for (int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[0])
                break;

            if (i == cpf.Length - 1)
                return true;
        }
        return false;
    }
    private bool ValidarCPF(string cpf)
    {
        if (cpf.Length != 11 || !cpf.All(char.IsDigit) || Numeros_Iguais(cpf)) return false;

        int[] pesos1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] pesos2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma1 = 0;
        int soma2 = 0;

        for (int i = 0; i < 9; i++)
        {
            soma1 += (cpf[i] - '0') * pesos1[i];
            soma2 += (cpf[i] - '0') * pesos2[i];
        }

        int Primeiro_Digito = soma1 % 11 < 2 ? 0 : 11 - soma1 % 11;

        soma2 += Primeiro_Digito * pesos2[9];

        int Segundo_Digito = soma2 % 11 < 2 ? 0 : 11 - soma2 % 11;

        return Primeiro_Digito == cpf[9] - '0' && Segundo_Digito == cpf[10] - '0';
    }

    private DateTime SolicitarDataNascimento()
    {
        DateTime dataNascimento;
        while (true)
        {
            Console.Write("Data de Nascimento: ");
            string dataNascimento_str = Console.ReadLine();

            if (!DateTime.TryParseExact(dataNascimento_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
            {
                Console.WriteLine("\nErro: data no formato incorreto.\n");
                continue;
            }

            if (dataNascimento >= DateTime.Now.AddYears(-13))
            {
                Console.WriteLine("\nErro: paciente deve ter pelo menos 13 anos.\n");
                continue;
            }

            break;
        }

        return dataNascimento;
    }

    /*public void ImprimirDados()
    {
        Console.WriteLine("\n=================================================================================\n");

        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"CPF: {CPF}");
        Console.WriteLine($"Data de Nascimento: {DataNascimento.ToString("dd/MM/yyyy")}");

        Console.WriteLine("\n=================================================================================\n");
    }*/
}

