using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagement.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoAntecedenteMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposAntecedentesMedicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAntecedentesMedicos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiposAntecedentesMedicos_Nombre",
                table: "TiposAntecedentesMedicos",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposAntecedentesMedicos");
        }
    }
}
