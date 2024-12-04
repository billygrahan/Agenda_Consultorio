using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda_Consultorio.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacaoAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_CPF",
                table: "Agendamentos",
                column: "CPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos",
                column: "CPF",
                principalTable: "Pacientes",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Pacientes_CPF",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_CPF",
                table: "Agendamentos");
        }
    }
}
