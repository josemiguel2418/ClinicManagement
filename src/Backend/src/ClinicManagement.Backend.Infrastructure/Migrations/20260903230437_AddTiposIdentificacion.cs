using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTiposIdentificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificacion",
                table: "Pacientes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoIdentificacionId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TiposIdentificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIdentificacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_TipoIdentificacionId_Identificacion",
                table: "Pacientes",
                columns: new[] { "TipoIdentificacionId", "Identificacion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposIdentificacion_Nombre",
                table: "TiposIdentificacion",
                column: "Nombre",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_TiposIdentificacion_TipoIdentificacionId",
                table: "Pacientes",
                column: "TipoIdentificacionId",
                principalTable: "TiposIdentificacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_TiposIdentificacion_TipoIdentificacionId",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "TiposIdentificacion");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_TipoIdentificacionId_Identificacion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Identificacion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacionId",
                table: "Pacientes");
        }
    }
}
