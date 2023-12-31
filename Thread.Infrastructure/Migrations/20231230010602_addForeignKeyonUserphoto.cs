using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyonUserphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_AppUser_AppUserId",
                table: "UserPhoto");

            migrationBuilder.DropIndex(
                name: "IX_UserPhoto_AppUserId",
                table: "UserPhoto");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserPhoto");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_UserId",
                table: "UserPhoto",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_AppUser_UserId",
                table: "UserPhoto",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhoto_AppUser_UserId",
                table: "UserPhoto");

            migrationBuilder.DropIndex(
                name: "IX_UserPhoto_UserId",
                table: "UserPhoto");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "UserPhoto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_AppUserId",
                table: "UserPhoto",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhoto_AppUser_AppUserId",
                table: "UserPhoto",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id");
        }
    }
}
