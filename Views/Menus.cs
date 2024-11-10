
namespace Agenda_Consultorio.Views;

public class Menus
{
    public static int MenuInicial()
    {
        int comando;

        Console.WriteLine("\n");
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1-Cadastro de pacientes");
        Console.WriteLine("2-Agenda");
        Console.WriteLine("3-Fim");
        Console.WriteLine("\n");
        Console.Write(">");

        string comando_str = Console.ReadLine();
        if (!(int.TryParse(comando_str, out comando)))
            return 0;
        else return comando;
    }

    public static int MenuPacientes()
    {
        int comando;

        Console.WriteLine("\n");
        Console.WriteLine("Menu do Cadastro de Pacientes");
        Console.WriteLine("1-Cadastrar novo paciente");
        Console.WriteLine("2-Excluir paciente");
        Console.WriteLine("3-Listar pacientes (ordenado por CPF)");
        Console.WriteLine("4-Listar pacientes (ordenado por nome)");
        Console.WriteLine("5-Voltar p/ menu principal");
        Console.WriteLine("\n");
        Console.Write(">");

        string comando_str = Console.ReadLine();
        if (!(int.TryParse(comando_str, out comando)))
            return 0;
        else return comando;
    }

    public static int MenuAgenda()
    {
        int comando;

        Console.WriteLine("\n");
        Console.WriteLine("1-Agendar consulta");
        Console.WriteLine("2-Cancelar agendamento");
        Console.WriteLine("3-Listar agenda");
        Console.WriteLine("4-Voltar p/ menu principal");
        Console.WriteLine("\n");
        Console.Write(">");

        string comando_str = Console.ReadLine();
        if (!(int.TryParse(comando_str, out comando)))
            return 0;
        else return comando;
    }
}
