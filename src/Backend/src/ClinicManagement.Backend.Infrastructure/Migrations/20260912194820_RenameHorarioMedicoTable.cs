using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameHorarioMedicoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorarioMedico_Consultorios_ConsultorioId",
                table: "HorarioMedico");

            migrationBuilder.DropForeignKey(
                name: "FK_HorarioMedico_Medicos_MedicoId",
                table: "HorarioMedico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorarioMedico",
                table: "HorarioMedico");

            migrationBuilder.RenameTable(
                name: "HorarioMedico",
                newName: "HorariosMedicos");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioMedico_MedicoId",
                table: "HorariosMedicos",
                newName: "IX_HorariosMedicos_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorarioMedico_ConsultorioId",
                table: "HorariosMedicos",
                newName: "IX_HorariosMedicos_ConsultorioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorariosMedicos",
                table: "HorariosMedicos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosMedicos_Consultorios_ConsultorioId",
                table: "HorariosMedicos",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HorariosMedicos_Medicos_MedicoId",
                table: "HorariosMedicos",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HorariosMedicos_Consultorios_ConsultorioId",
                table: "HorariosMedicos");

            migrationBuilder.DropForeignKey(
                name: "FK_HorariosMedicos_Medicos_MedicoId",
                table: "HorariosMedicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HorariosMedicos",
                table: "HorariosMedicos");

            migrationBuilder.RenameTable(
                name: "HorariosMedicos",
                newName: "HorarioMedico");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosMedicos_MedicoId",
                table: "HorarioMedico",
                newName: "IX_HorarioMedico_MedicoId");

            migrationBuilder.RenameIndex(
                name: "IX_HorariosMedicos_ConsultorioId",
                table: "HorarioMedico",
                newName: "IX_HorarioMedico_ConsultorioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HorarioMedico",
                table: "HorarioMedico",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioMedico_Consultorios_ConsultorioId",
                table: "HorarioMedico",
                column: "ConsultorioId",
                principalTable: "Consultorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HorarioMedico_Medicos_MedicoId",
                table: "HorarioMedico",
                column: "MedicoId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
