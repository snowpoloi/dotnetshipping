using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingCalculator.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostalCodesAndOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carrier",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Zone",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Offers",
                newName: "MinimumWeight");

            migrationBuilder.UpdateData(
                table: "Offers",
                keyColumn: "OfferType",
                keyValue: null,
                column: "OfferType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "OfferType",
                table: "Offers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseCost",
                table: "Offers",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CubicMeterCost",
                table: "Offers",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraCostPerKg",
                table: "Offers",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumWeight",
                table: "Offers",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumShippingCost",
                table: "Offers",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PostalCodeOffers",
                columns: table => new
                {
                    PostalCodeId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeOffers", x => new { x.PostalCodeId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_PostalCodeOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostalCodeOffers_PostalCodes_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CarrierId",
                table: "Offers",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodeOffers_OfferId",
                table: "PostalCodeOffers",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Carriers_CarrierId",
                table: "Offers",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Carriers_CarrierId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "PostalCodeOffers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CarrierId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "BaseCost",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CubicMeterCost",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ExtraCostPerKg",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "MaximumWeight",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "MinimumShippingCost",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "MinimumWeight",
                table: "Offers",
                newName: "Rate");

            migrationBuilder.AlterColumn<string>(
                name: "OfferType",
                table: "Offers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Carrier",
                table: "Offers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Zone",
                table: "Offers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
