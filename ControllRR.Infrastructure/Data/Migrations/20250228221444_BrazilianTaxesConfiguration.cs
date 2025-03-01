using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BrazilianTaxesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CFOPId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CofinsAmount",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CofinsBase",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreightMode",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IcmsAmount",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IcmsBase",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerCNPJ",
                table: "PurchaseOrders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IssuerIE",
                table: "PurchaseOrders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NFeSourceId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NFeStatus",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OperationType",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PisAmount",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PisBase",
                table: "PurchaseOrders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNFeKey",
                table: "PurchaseOrders",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CFOPs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo_Cfop = table.Column<double>(type: "double", nullable: true),
                    Desc_Cfop = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CFOPs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CNAEs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo_Cnae = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc_Cnae = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNAEs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "COFINs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo_Cofins = table.Column<int>(type: "int", nullable: false),
                    Desc_Cofins = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COFINs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CSOSNs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo_Csosn = table.Column<int>(type: "int", nullable: true),
                    Name_Csosn = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc_Csosn = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSOSNs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ICMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Icms = table.Column<int>(type: "int", nullable: true),
                    Codigo_Icms = table.Column<int>(type: "int", nullable: true),
                    Desc_Icms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IcmsDesoneracaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Icms_Desoneracao = table.Column<int>(type: "int", nullable: true),
                    Codigo_Icms_Desoneracao = table.Column<int>(type: "int", nullable: true),
                    Desc_Icms_Desoneracao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmsDesoneracaos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IcmsModalidadeBCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Icms_Modalidade_bc = table.Column<int>(type: "int", nullable: true),
                    Codigo_Icms_Modalidade_bc = table.Column<int>(type: "int", nullable: true),
                    Desc_Icms_Modalidade_bc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmsModalidadeBCs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IcmsModalidadeSTs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Icms_Modalidade_st = table.Column<int>(type: "int", nullable: true),
                    Codigo_Icms_Modalidade_st = table.Column<int>(type: "int", nullable: true),
                    Desc_Icms_Modalidade_st = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmsModalidadeSTs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IcmsOrigems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Icms_Origem = table.Column<int>(type: "int", nullable: false),
                    Codigo_Icms_Origem = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc_Icms_Origem = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcmsOrigems", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IpiOperacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Ipi_Operacao = table.Column<int>(type: "int", nullable: false),
                    Nome_Ipi_Operacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpiOperacoes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IPIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Ipi = table.Column<int>(type: "int", nullable: false),
                    Cod_Ipi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Desc_Ipi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPIs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NCMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Ncm = table.Column<int>(type: "int", nullable: false),
                    Cod_Ncm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome_Ncm = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCMs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NFeSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NFeTypeOperation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFeSources", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PIS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Pis = table.Column<int>(type: "int", nullable: false),
                    Cod_Pis = table.Column<int>(type: "int", nullable: false),
                    Desc_Pis = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PIS", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IpiEnquadramentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Ipi_Enquadramento = table.Column<int>(type: "int", nullable: false),
                    Desc_Ipi_Enquadramento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo_Ipi_Enquadramento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Ipi_Operacao = table.Column<int>(type: "int", nullable: false),
                    IpiOperacaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpiEnquadramentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpiEnquadramentos_IpiOperacoes_IpiOperacaoId",
                        column: x => x.IpiOperacaoId,
                        principalTable: "IpiOperacoes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CFOPId",
                table: "PurchaseOrders",
                column: "CFOPId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_NFeSourceId",
                table: "PurchaseOrders",
                column: "NFeSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_IpiEnquadramentos_IpiOperacaoId",
                table: "IpiEnquadramentos",
                column: "IpiOperacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_CFOPs_CFOPId",
                table: "PurchaseOrders",
                column: "CFOPId",
                principalTable: "CFOPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_NFeSources_NFeSourceId",
                table: "PurchaseOrders",
                column: "NFeSourceId",
                principalTable: "NFeSources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_CFOPs_CFOPId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_NFeSources_NFeSourceId",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "CFOPs");

            migrationBuilder.DropTable(
                name: "CNAEs");

            migrationBuilder.DropTable(
                name: "COFINs");

            migrationBuilder.DropTable(
                name: "CSOSNs");

            migrationBuilder.DropTable(
                name: "ICMs");

            migrationBuilder.DropTable(
                name: "IcmsDesoneracaos");

            migrationBuilder.DropTable(
                name: "IcmsModalidadeBCs");

            migrationBuilder.DropTable(
                name: "IcmsModalidadeSTs");

            migrationBuilder.DropTable(
                name: "IcmsOrigems");

            migrationBuilder.DropTable(
                name: "IpiEnquadramentos");

            migrationBuilder.DropTable(
                name: "IPIs");

            migrationBuilder.DropTable(
                name: "NCMs");

            migrationBuilder.DropTable(
                name: "NFeSources");

            migrationBuilder.DropTable(
                name: "PIS");

            migrationBuilder.DropTable(
                name: "IpiOperacoes");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_CFOPId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_NFeSourceId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CFOPId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CofinsAmount",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "CofinsBase",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "FreightMode",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IcmsAmount",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IcmsBase",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IssuerCNPJ",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IssuerIE",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "NFeSourceId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "NFeStatus",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "OperationType",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PisAmount",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "PisBase",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ReferenceNFeKey",
                table: "PurchaseOrders");
        }
    }
}
