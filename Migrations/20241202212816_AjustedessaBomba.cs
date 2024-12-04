using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_Consultorio.Migrations
{
    /// <inheritdoc />
    public partial class AjustedessaBomba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_PacienteCPF",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "PacienteCPF",
                table: "Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos",
                column: "CPF",
                principalTable: "Pacientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "PacienteCPF",
                table: "Agendamentos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_PacienteCPF",
                table: "Agendamentos",
                column: "PacienteCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos",
                column: "CPF",
                principalTable: "Pacientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_PacienteCPF",
                table: "Agendamentos",
                column: "PacienteCPF",
                principalTable: "Pacientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
