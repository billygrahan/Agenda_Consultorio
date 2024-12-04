using Agenda_Consultorio.Views;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Agenda_Consultorio.Validations;

public class ValidationsPaciente
{
    public static bool ValidaNome(string nome)
    {
        if (nome.Length < 5)
        {
            Errors.MensagemdeErro("nome maior que 5");
            return false;
        }
        return true;
    }
    public static bool ValidaCPF(string cpf, List<string> CPFs)
    {
        if (!ValidarCPF(cpf))
        {
            Errors.MensagemdeErro("cpf invalido");
            return false;
        }
        else if (CPFs.Contains(cpf))
        {
            Errors.MensagemdeErro("cpf ja exixte");
            return false;
        }
        return true;
    }

    public static bool ValidaDataNascimento(string dataNascimento_str)
    {
        DateTime dataNascimento;
        if (!DateTime.TryParseExact(dataNascimento_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNascimento))
        {
            Errors.MensagemdeErro("DateTime form");
            return false;
        }

        if (dataNascimento >= DateTime.Now.AddYears(-13))
        {
            Errors.MensagemdeErro("idade paciente");
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

