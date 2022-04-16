using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class AddSurveyInfrastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    СreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_User_СreatorId",
                        column: x => x.СreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVariantsQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    CanChooseManyAnswers = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVariantsQuestion", x => new { x.Id, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_AnswerVariantsQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAnswerQuestion", x => new { x.Id, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_FileAnswerQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScaleAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    ScaleMinValue = table.Column<int>(type: "int", nullable: false),
                    ScaleMaxValue = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleAnswerQuestion", x => new { x.Id, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_ScaleAnswerQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StarRatingAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfStars = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarRatingAnswerQuestion", x => new { x.Id, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_StarRatingAnswerQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnswerQuestion", x => new { x.Id, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_TextAnswerQuestion_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVariant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVariant", x => new { x.Id, x.QuestionId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_AnswerVariant_AnswerVariantsQuestion_QuestionId_SurveyId",
                        columns: x => new { x.QuestionId, x.SurveyId },
                        principalTable: "AnswerVariantsQuestion",
                        principalColumns: new[] { "Id", "SurveyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEL+wdIKSDIdwu/EXFL4yHRxnteaUANJXrB+BtR7VRuhlTiEi/ja0WGoXQP1/vS5P/g==");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVariant_QuestionId_SurveyId",
                table: "AnswerVariant",
                columns: new[] { "QuestionId", "SurveyId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVariantsQuestion_SurveyId",
                table: "AnswerVariantsQuestion",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAnswerQuestion_SurveyId",
                table: "FileAnswerQuestion",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_ScaleAnswerQuestion_SurveyId",
                table: "ScaleAnswerQuestion",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_StarRatingAnswerQuestion_SurveyId",
                table: "StarRatingAnswerQuestion",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_СreatorId",
                table: "Survey",
                column: "СreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TextAnswerQuestion_SurveyId",
                table: "TextAnswerQuestion",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerVariant");

            migrationBuilder.DropTable(
                name: "FileAnswerQuestion");

            migrationBuilder.DropTable(
                name: "ScaleAnswerQuestion");

            migrationBuilder.DropTable(
                name: "StarRatingAnswerQuestion");

            migrationBuilder.DropTable(
                name: "TextAnswerQuestion");

            migrationBuilder.DropTable(
                name: "AnswerVariantsQuestion");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELfT5cA209WkUA68d/VfeM0qcd3nPcCAzWyPRqocEefzQ+bBcPH9NTW9RTxdxsyuCQ==");
        }
    }
}
