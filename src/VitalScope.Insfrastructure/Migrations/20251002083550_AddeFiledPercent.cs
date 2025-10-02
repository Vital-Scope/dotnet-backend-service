using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalScope.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddeFiledPercent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Percent",
                schema: "vital",
                table: "StudyMetas",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                schema: "vital",
                table: "StudyMetas");
        }
    }
}
