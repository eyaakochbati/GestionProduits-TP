using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionProduits.Migrations
{
    /// <inheritdoc />
    public partial class RestrictCategorieDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categories_CategorieId",
                table: "Produits");

            migrationBuilder.AddColumn<int>(
                name: "CategorieId1",
                table: "Produits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_CategorieId1",
                table: "Produits",
                column: "CategorieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categories_CategorieId",
                table: "Produits",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categories_CategorieId1",
                table: "Produits",
                column: "CategorieId1",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categories_CategorieId",
                table: "Produits");

            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categories_CategorieId1",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_CategorieId1",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "CategorieId1",
                table: "Produits");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categories_CategorieId",
                table: "Produits",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
