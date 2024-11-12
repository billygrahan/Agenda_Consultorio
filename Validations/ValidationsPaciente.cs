using System.Globalization;

namespace Agenda_Consultorio.Validations;

public class ValidationsPaciente
{
    protected bool ValidaNome(string nome)
    {
        if (nome.Length < 5)
        {
            Console.WriteLine("\nErro: o nome deve ter pelo menos 5 caracteres.\n");
            return false;
        }
        return true;
    }
    protected bool ValidaCPF(string cpf, List<string> CPFs)
    {
        if (!ValidarCPF(cpf))
        {
            Console.WriteLine("\nErro: CPF inválido\n");
            return false;
        }
        else if (CPFs.Contains(cpf))
        {
            Console.WriteLine("\nErro: CPF já cadastrado\n");
            return false;
        }
        return true;
    }

    protected bool ValidaDataNascimento(string dataNascimento_str)
    {
        DateTime dataNascimento;
        if (!DateTime.TryParseExact(dataNascimento_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
        {
            Console.WriteLine("\nErro: data no formato incorreto.\n");
            return false;
        }

        if (dataNascimento >= DateTime.Now.AddYears(-13))
        {
            Console.WriteLine("\nErro: paciente deve ter pelo menos 13 anos.\n");
            return false;
        }
        return true;
    }
    //------------------------------------------------------------- aux --------------------------------------------------------------------------//
    private static bool Numeros_Iguais(string cpf)
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
    private static bool ValidarCPF(string cpf)
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
}
