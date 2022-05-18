using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class AddUserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnswerVariantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswer_AnswerVariant_AnswerVariantId",
                        column: x => x.AnswerVariantId,
                        principalTable: "AnswerVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEL9G0xvpZ2d0ou4YB9D5W6ShltwRBSRJQDoZrG8/rTHTKNIJm7TZRovKpcgos/HPvg==");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_AnswerVariantId",
                table: "UserAnswer",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEA8M7189kTA4Dmjh3f/OWj6KLgYSUI4EelUsFdP0k7XOAbuUw7hQm15H2qz3aSTIpw==");
        }
    }
}
