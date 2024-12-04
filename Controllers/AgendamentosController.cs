using Agenda_Consultorio.Context;
using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
namespace Agenda_Consultorio.Controllers;

public class AgendamentosController
{
    private readonly AppDbContext _context;

    public AgendamentosController(AppDbContext context)
    {
        _context = context;
    }

    public bool PostAgendamento(Agendamento agendamento)
    {
        try
        {
            _context.Agendamentos.Add(agendamento);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            Errors.MensagemdeErro("insert BD");
            return false;
        }
    }

    public bool DeleteAgendamento(Agendamento agendamento)
    {
        try
        {
            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            Errors.MensagemdeErro("delete BD");
            return false;
        }
    }
}
