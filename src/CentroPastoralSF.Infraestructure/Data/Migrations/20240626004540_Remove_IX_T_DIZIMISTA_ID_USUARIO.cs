using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroPastoralSF.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Remove_IX_T_DIZIMISTA_ID_USUARIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_DIZIMISTA_ID_USUARIO",
                table: "T_DIZIMISTA");

            migrationBuilder.CreateIndex(
                name: "IX_T_DIZIMISTA_ID_USUARIO",
                table: "T_DIZIMISTA",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_DIZIMISTA_ID_USUARIO",
                table: "T_DIZIMISTA");

            migrationBuilder.CreateIndex(
                name: "IX_T_DIZIMISTA_ID_USUARIO",
                table: "T_DIZIMISTA",
                column: "ID_USUARIO",
                unique: true);
        }
    }
}
