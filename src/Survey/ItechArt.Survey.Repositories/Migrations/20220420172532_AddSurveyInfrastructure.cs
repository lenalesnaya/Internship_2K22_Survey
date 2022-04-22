using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class AddSurveyInfrastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfSurvey",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Survey_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVariantsQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CanChooseManyAnswers = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVariantsQuestion", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAnswerQuestion", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScaleMinValue = table.Column<int>(type: "int", nullable: false),
                    ScaleMaxValue = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScaleAnswerQuestion", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountOfStars = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarRatingAnswerQuestion", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SurveyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnswerQuestion", x => x.Id);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerVariant_AnswerVariantsQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "AnswerVariantsQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEMN9cPkirO4Bmrt8Gf/NKtSBPBN3FoDnteZ+5O7xmVxHoAGM1bXQXFAmMR5WvbJfwg==");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVariant_QuestionId",
                table: "AnswerVariant",
                column: "QuestionId");

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
                name: "IX_Survey_CreatorId",
                table: "Survey",
                column: "CreatorId");

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

            migrationBuilder.AddColumn<int>(
                name: "AmountOfSurvey",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAELx9KTCniO6cGP0iS/TPb2wkDWaHLVa5NoEEkoSkNGJp6c3VIR9gi9aXN68aorJLag==");
        }
    }
}
