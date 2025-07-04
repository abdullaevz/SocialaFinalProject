using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class editedMessageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_RecieberId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecieberId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecieberId",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages",
                column: "RecieverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_RecieverId",
                table: "Messages",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_RecieverId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecieverId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "RecieberId",
                table: "Messages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecieberId",
                table: "Messages",
                column: "RecieberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_RecieberId",
                table: "Messages",
                column: "RecieberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
