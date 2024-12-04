using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_Consultorio.Migrations
{
    /// <inheritdoc />
    public partial class AjusteChaveSecundariaAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos");

            migrationBuilder.AlterColumn<string>(
                name: "PacienteCPF",
                table: "Agendamentos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos",
                column: "PacienteCPF",
                principalTable: "Pacientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos");

            migrationBuilder.AlterColumn<string>(
                name: "PacienteCPF",
                table: "Agendamentos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos",
                column: "PacienteCPF",
                principalTable: "Pacientes",
                principalColumn: "CPF");
        }
    }
}
