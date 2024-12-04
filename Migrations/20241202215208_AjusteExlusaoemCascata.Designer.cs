﻿// <auto-generated />
using System;
using Agenda_Consultorio.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Agenda_Consultorio.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241202215208_AjusteExlusaoemCascata")]
    partial class AjusteExlusaoemCascata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Agenda_Consultorio.Models.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataConsulta")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("HoraFinal")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("HoraInicial")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.HasIndex("CPF");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("Agenda_Consultorio.Models.Paciente", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("text");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CPF");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Agenda_Consultorio.Models.Agendamento", b =>
                {
                    b.HasOne("Agenda_Consultorio.Models.Paciente", "Paciente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("CPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("Agenda_Consultorio.Models.Paciente", b =>
                {
                    b.Navigation("Agendamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
