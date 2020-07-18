using Microsoft.EntityFrameworkCore.Migrations;

namespace SupermarketApp.Migrations
{
    public partial class EditedSaleDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetails_ProductId",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SalesDetails");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SalesDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "SalesDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SalesDetails");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SalesDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SalesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetails_ProductId",
                table: "SalesDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Products_ProductId",
                table: "SalesDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
