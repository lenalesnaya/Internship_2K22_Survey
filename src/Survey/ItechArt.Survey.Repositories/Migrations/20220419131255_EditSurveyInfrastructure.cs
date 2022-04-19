using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItechArt.Survey.Repositories.Migrations
{
    public partial class EditSurveyInfrastructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountOfSurvey",
                table: "User",
                newName: "AmountOfSurveys");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfLastUpdating",
                table: "Survey",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGQiqgnDtkNDf1xMmH6zo6PuSWYQnfcMBEGV6y3TWtT/UThKoQDHdYxTGjlFOaGZ6w==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfLastUpdating",
                table: "Survey");

            migrationBuilder.RenameColumn(
                name: "AmountOfSurveys",
                table: "User",
                newName: "AmountOfSurvey");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1,
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECPJNd+tTRrdZWTAC6tQwLvVLTOCwFErrCgV2ubZHpVOzGihKCzl4PHtxGqc9E4WNA==");
        }
    }
}
