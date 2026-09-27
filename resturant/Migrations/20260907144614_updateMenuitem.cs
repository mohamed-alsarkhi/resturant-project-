using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resturant.Migrations
{
    /// <inheritdoc />
    public partial class updateMenuitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryid",
                table: "Menu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_categoryid",
                table: "Menu",
                column: "categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_categoryid",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "categoryid",
                table: "Menu");
        }
    }
}
