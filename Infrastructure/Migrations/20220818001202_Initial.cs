using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadBoletos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataOperacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CodigoCliente = table.Column<string>(type: "TEXT", nullable: false),
                    TipoOperacao = table.Column<string>(type: "TEXT", nullable: false),
                    IdBolsa = table.Column<string>(type: "TEXT", nullable: false),
                    CodigoAtivo = table.Column<string>(type: "TEXT", nullable: false),
                    Corretora = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadBoletos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadBoletos");
        }
    }
}
