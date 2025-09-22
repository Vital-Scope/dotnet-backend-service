using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalScope.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDiagnose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                schema: "vital",
                table: "StudyMetas",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diagnosis",
                schema: "vital",
                table: "StudyMetas");
        }
    }
}
