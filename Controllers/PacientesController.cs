using Agenda_Consultorio.Context;
using Agenda_Consultorio.Models;
using Agenda_Consultorio.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Consultorio.Controllers;

public class PacientesController
{
    private readonly AppDbContext _context;

    public PacientesController(AppDbContext context)
    {
        _context = context;
    }

    public bool PostPaciente(Paciente paciente)
    {
        try
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            Errors.MensagemdeErro("insert BD");
            return false;
        }
    }



    public bool DeletePaciente(Paciente paciente)
    {
        try
        {
            _context.Pacientes.Remove(paciente);
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
