using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroPastoralSF.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmail_Dizimista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "END_EMAIL",
                table: "T_USUARIO",
                newName: "EML_EMAIL");

            migrationBuilder.AddColumn<string>(
                name: "EML_EMAIL",
                table: "T_DIZIMISTA",
                type: "NVARCHAR(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EML_EMAIL",
                table: "T_DIZIMISTA");

            migrationBuilder.RenameColumn(
                name: "EML_EMAIL",
                table: "T_USUARIO",
                newName: "END_EMAIL");
        }
    }
}
