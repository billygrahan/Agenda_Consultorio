using Agenda_Consultorio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Agenda_Consultorio.Views;

public class Errors
{
    private static Dictionary<string, string> erros = new Dictionary<string, string>()
    {
        {"paciente nao cadastrado", "paciente não cadastrado."},
        {"paciente agendado", "paciente está agendado."},
        {"consulta marcada", "Paciente já tem uma consulta marcada"},
        {"new agendamento is null", "Agendamento é nulo"},
        {"agendamento nao encontrado", "agendamento não encontrado" },
        {"DateTime form", "Data no formato incorreto." },
        {"data no passado", "A data da consulta deve ser para um período futuro." },
        {"TimeSpan form", "Hora inicial inválida. Use o formato HHMM."},
        {"expediente", "Horário fora do expediente (8:00 às 19:00)."},
        {"TimeSpan multiple 15", "Horas devem ser múltiplos de 15 minutos (e.g., 1400, 1415, 1430, etc.)."},
        {"final maior que inicial", "Hora final deve ser após a Hora inicial."},
        {"colisao entre consultas", "já existe uma consulta agendada nesse horário."},
        {"nome maior que 5", "o nome deve ter pelo menos 5 caracteres."},
        {"cpf invalido", "CPF inválido."},
        {"cpf ja exixte", "CPF já cadastrado" },
        {"idade paciente", "paciente deve ter pelo menos 13 anos."},
        {"insert BD", "Falha ao inserir elemento no Banco de dados."},
        {"delete BD", "Falha ao excluir elemento no Banco de dados."},
        {"Comand-incorrect", "Comando não Reconhecido."}
    };

    public static void MensagemdeErro(string erro)
    {
        if (erros.TryGetValue(erro, out string mensagem))
        {
            Console.WriteLine($"\nErro: {mensagem}\n");
        }
        else
        {
            Console.WriteLine(erro);
        }
    }
}
