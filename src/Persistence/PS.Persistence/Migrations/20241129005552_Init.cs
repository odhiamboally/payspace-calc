using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    TaxRegime = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBrackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxBrackets_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false),
                    CalculatedTax = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxCalculations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    RateCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRates_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxBracketLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxBracketId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    LowerLimit = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false),
                    UpperLimit = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(31,12)", precision: 31, scale: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBracketLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxBracketLines_TaxBrackets_TaxBracketId",
                        column: x => x.TaxBracketId,
                        principalTable: "TaxBrackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxBracketLines_TaxBracketId",
                table: "TaxBracketLines",
                column: "TaxBracketId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxBrackets_CountryId",
                table: "TaxBrackets",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxCalculations_CountryId",
                table: "TaxCalculations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxRates_CountryId",
                table: "TaxRates",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBracketLines");

            migrationBuilder.DropTable(
                name: "TaxCalculations");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "TaxBrackets");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
