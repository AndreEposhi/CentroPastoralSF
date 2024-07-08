using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CentroPastoralSF.Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_NOME = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    NOM_SOBRENOME = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    END_EMAIL = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    SNH_SENHA = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    DAT_DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DAT_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_USUARIO_ID_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "T_DIZIMISTA",
                columns: table => new
                {
                    ID_DIZIMISTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOM_NOME = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    NOM_SOBRENOME = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    NUM_DDD = table.Column<string>(type: "NVARCHAR(2)", maxLength: 2, nullable: false),
                    NUM_NUMERO = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    END_LOGRADOURO = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    END_NUMERO = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    END_COMPLEMENTO = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    END_BAIRRO = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    END_MUNICIPIO = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    END_UF = table.Column<string>(type: "NVARCHAR(2)", maxLength: 2, nullable: false),
                    END_CEP = table.Column<string>(type: "NVARCHAR(8)", maxLength: 8, nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    DAT_DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DAT_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_DIZIMISTA_ID_DIZIMISTA", x => x.ID_DIZIMISTA);
                    table.ForeignKey(
                        name: "FK_T_DIZIMISTA_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "T_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_DIZIMISTA_ID_USUARIO",
                table: "T_DIZIMISTA",
                column: "ID_USUARIO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_DIZIMISTA");

            migrationBuilder.DropTable(
                name: "T_USUARIO");
        }
    }
}
