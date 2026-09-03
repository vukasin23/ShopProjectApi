using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RightWayForLineBetweenInventoryAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryProduct");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Inventories");

            migrationBuilder.CreateTable(
                name: "InventoryProduct",
                columns: table => new
                {
                    InventoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProduct", x => new { x.InventoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Inventories_InventoriesId",
                        column: x => x.InventoriesId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_ProductsId",
                table: "InventoryProduct",
                column: "ProductsId");
        }
    }
}
