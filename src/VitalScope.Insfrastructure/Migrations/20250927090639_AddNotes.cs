using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalScope.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "vital",
                table: "StudyMetas",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "vital",
                table: "StudyMetas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "vital",
                table: "StudyMetas",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "vital",
                table: "StudyMetas");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "vital",
                table: "StudyMetas");
        }
    }
}
