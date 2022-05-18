using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class AddAvatarColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarFilePath",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEA8M7189kTA4Dmjh3f/OWj6KLgYSUI4EelUsFdP0k7XOAbuUw7hQm15H2qz3aSTIpw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarFilePath",
                table: "User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEEVgJ3hbmcv//WnOcWQrqil0S2vvAS4pUfXAKCNQgSYZ2/Idk3JV8pR8kyphbeloiw==");
        }
    }
}
