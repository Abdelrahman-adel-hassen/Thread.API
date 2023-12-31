using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thread.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeuserNameAndCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppUser",
                newName: "Country");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AppUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "AppUser",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AppUser",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
