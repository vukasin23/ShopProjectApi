using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Table_Product_Specification_Naming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ProductSpecifications",
                newName: "SpecificationValue");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ProductSpecifications",
                newName: "SpecificationName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecificationValue",
                table: "ProductSpecifications",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "SpecificationName",
                table: "ProductSpecifications",
                newName: "Key");
        }
    }
}
