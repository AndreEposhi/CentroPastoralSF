using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroPastoralSF.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dizmista_Endereco_Complemento_Nulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "END_COMPLEMENTO",
                table: "T_DIZIMISTA",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "END_COMPLEMENTO",
                table: "T_DIZIMISTA",
                type: "NVARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
