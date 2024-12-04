using Agenda_Consultorio.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda_Consultorio.Context;

public class AppDbContext : DbContext
{
    public DbSet<Agendamento>? Agendamentos { get; set; }
    public DbSet<Paciente>? Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        string host = "localhost";
        //string port = "5432";
        string username = "postgres";
        string password = "@marelO50";

        string database = "ApiConsultórioBD";

        string connectionString = $"Host={host};Username={username};Password={password};Database={database}";

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Paciente) 
            .WithMany(p => p.Agendamentos) 
            .HasForeignKey(a => a.CPF) 
            .OnDelete(DeleteBehavior.Cascade); 
    }


}
