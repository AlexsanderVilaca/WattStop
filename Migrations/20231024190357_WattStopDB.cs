using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIClient.Migrations
{
    /// <inheritdoc />
    public partial class WattStopDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Avaliacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estrelas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PontoRecargaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPontoRecarga",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    PontoRecargaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPontoRecarga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PontoRecarga",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoCarregador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontoRecarga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QrCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Conteudo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PontoRecargaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QrCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DT_Criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TP_Acesso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "HistoricoPontoRecarga");

            migrationBuilder.DropTable(
                name: "PontoRecarga");

            migrationBuilder.DropTable(
                name: "QrCode");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
