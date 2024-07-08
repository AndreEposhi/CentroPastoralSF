using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroPastoralSF.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataNascimento_Dizimista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DAT_DATA_NASCIMENTO",
                table: "T_DIZIMISTA",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DAT_DATA_NASCIMENTO",
                table: "T_DIZIMISTA");
        }
    }
}
