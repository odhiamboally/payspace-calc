using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Default_decimal_zero_CalculatedTax_And_NetPay_TaxCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NetPay",
                table: "TaxCalculations",
                type: "decimal(31,12)",
                precision: 31,
                scale: 12,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(31,12)",
                oldPrecision: 31,
                oldScale: 12);

            migrationBuilder.AlterColumn<decimal>(
                name: "CalculatedTax",
                table: "TaxCalculations",
                type: "decimal(31,12)",
                precision: 31,
                scale: 12,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(31,12)",
                oldPrecision: 31,
                oldScale: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NetPay",
                table: "TaxCalculations",
                type: "decimal(31,12)",
                precision: 31,
                scale: 12,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(31,12)",
                oldPrecision: 31,
                oldScale: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CalculatedTax",
                table: "TaxCalculations",
                type: "decimal(31,12)",
                precision: 31,
                scale: 12,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(31,12)",
                oldPrecision: 31,
                oldScale: 12,
                oldNullable: true);
        }
    }
}
