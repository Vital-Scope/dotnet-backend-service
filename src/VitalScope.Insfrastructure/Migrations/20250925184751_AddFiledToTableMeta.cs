using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalScope.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFiledToTableMeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "vital",
                table: "StudyMetas",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                schema: "vital",
                table: "StudyMetas",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PregnancyWeek",
                schema: "vital",
                table: "StudyMetas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Result",
                schema: "vital",
                table: "StudyMetas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "vital",
                table: "StudyMetas",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.DropColumn(
                name: "PregnancyWeek",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                schema: "vital",
                table: "StudyMetas",
                newName: "Date");
        }
    }
}
