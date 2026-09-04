using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAntecedentesMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntecedentesMedico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAntecedenteMedicoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntecedentesMedico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AntecedentesMedico_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntecedentesMedico_TiposAntecedentesMedicos_TipoAntecedenteMedicoId",
                        column: x => x.TipoAntecedenteMedicoId,
                        principalTable: "TiposAntecedentesMedicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedico_PacienteId",
                table: "AntecedentesMedico",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AntecedentesMedico_TipoAntecedenteMedicoId",
                table: "AntecedentesMedico",
                column: "TipoAntecedenteMedicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntecedentesMedico");
        }
    }
}
