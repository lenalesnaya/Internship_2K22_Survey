using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class AddSelectedAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SelectedAnswer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerVariantId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AnswerVariantsQuestionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedAnswer_AnswerVariant_AnswerVariantId",
                        column: x => x.AnswerVariantId,
                        principalTable: "AnswerVariant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedAnswer_AnswerVariantsQuestion_AnswerVariantsQuestionId",
                        column: x => x.AnswerVariantsQuestionId,
                        principalTable: "AnswerVariantsQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedAnswer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELq20q3fGYaxH96swbY5mmU8/wsJnsHTByPrc3DRJCTYpsDCGYIHHAjFvFYn0nH4Og==");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedAnswer_AnswerVariantId",
                table: "SelectedAnswer",
                column: "AnswerVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedAnswer_AnswerVariantsQuestionId",
                table: "SelectedAnswer",
                column: "AnswerVariantsQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedAnswer_UserId",
                table: "SelectedAnswer",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedAnswer");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEA8M7189kTA4Dmjh3f/OWj6KLgYSUI4EelUsFdP0k7XOAbuUw7hQm15H2qz3aSTIpw==");
        }
    }
}
