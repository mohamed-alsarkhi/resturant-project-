using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace resturant.Migrations
{
    /// <inheritdoc />
    public partial class imageurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "categoryid",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "categoryid",
                table: "Menu",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Categories_categoryid",
                table: "Menu",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}