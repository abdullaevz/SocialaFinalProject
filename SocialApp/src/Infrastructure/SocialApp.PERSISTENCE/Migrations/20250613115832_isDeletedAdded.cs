using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class isDeletedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Stories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Posts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Comments");
        }
    }
}
