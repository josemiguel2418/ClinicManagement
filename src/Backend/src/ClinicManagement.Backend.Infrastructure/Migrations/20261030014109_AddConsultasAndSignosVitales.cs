using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultasAndSignosVitales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitaId = table.Column<int>(type: "int", nullable: false),
                    Sintomas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tratamiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SignosVitales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<int>(type: "int", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    FrecuenciaCardiaca = table.Column<int>(type: "int", nullable: true),
                    Temperatura = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    PresionSistolica = table.Column<int>(type: "int", nullable: true),
                    PresionDiastolica = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignosVitales", x => x.Id);
                    table.CheckConstraint("CK_SignosVitales_AlMenosUnaMedicion", "[Altura] IS NOT NULL\r\nOR [Peso] IS NOT NULL\r\nOR [FrecuenciaCardiaca] IS NOT NULL\r\nOR [Temperatura] IS NOT NULL\r\nOR [PresionSistolica] IS NOT NULL");
                    table.CheckConstraint("CK_SignosVitales_AlturaPositiva", "[Altura] IS NULL OR [Altura] > 0");
                    table.CheckConstraint("CK_SignosVitales_FrecuenciaCardiacaPositiva", "[FrecuenciaCardiaca] IS NULL OR [FrecuenciaCardiaca] >= 0");
                    table.CheckConstraint("CK_SignosVitales_PesoPositivo", "[Peso] IS NULL OR [Peso] > 0");
                    table.CheckConstraint("CK_SignosVitales_PresionCompleta", "([PresionSistolica] IS NULL AND [PresionDiastolica] IS NULL)\r\nOR\r\n([PresionSistolica] IS NOT NULL AND [PresionDiastolica] IS NOT NULL)");
                    table.CheckConstraint("CK_SignosVitales_PresionPositiva", "([PresionSistolica] IS NULL OR [PresionSistolica] > 0)\r\nAND\r\n([PresionDiastolica] IS NULL OR [PresionDiastolica] > 0)");
                    table.CheckConstraint("CK_SignosVitales_TemperaturaPositiva", "[Temperatura] IS NULL OR [Temperatura] > 0");
                    table.ForeignKey(
                        name: "FK_SignosVitales_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_CitaId",
                table: "Consultas",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignosVitales_ConsultaId",
                table: "SignosVitales",
                column: "ConsultaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignosVitales");

            migrationBuilder.DropTable(
                name: "Consultas");
        }
    }
}
