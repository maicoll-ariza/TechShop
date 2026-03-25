using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoriesTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catesgories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catesgories",
                table: "Catesgories");

            migrationBuilder.RenameTable(
                name: "Catesgories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Catesgories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catesgories",
                table: "Catesgories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catesgories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Catesgories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
