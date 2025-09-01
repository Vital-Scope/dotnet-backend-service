using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VitalScope.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "vital");

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyMetas",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NumSignals = table.Column<int>(type: "integer", nullable: true),
                    SamplingFrequency = table.Column<int>(type: "integer", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Ph = table.Column<float>(type: "real", nullable: true),
                    BDecf = table.Column<float>(type: "real", nullable: true),
                    PCO2 = table.Column<float>(type: "real", nullable: true),
                    BE = table.Column<float>(type: "real", nullable: true),
                    Apgar1 = table.Column<float>(type: "real", nullable: true),
                    Apgar5 = table.Column<float>(type: "real", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gravidity = table.Column<int>(type: "integer", nullable: true),
                    Parity = table.Column<int>(type: "integer", nullable: true),
                    Diabetes = table.Column<int>(type: "integer", nullable: true),
                    Presentation = table.Column<int>(type: "integer", nullable: true),
                    Istage = table.Column<int>(type: "integer", nullable: true),
                    IIstage = table.Column<int>(type: "integer", nullable: true),
                    Delivtype = table.Column<int>(type: "integer", nullable: true),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyMetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyMetas_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "vital",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyMain",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Channel = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    StudyMetaInformationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyMain_StudyMetas_StudyMetaInformationId",
                        column: x => x.StudyMetaInformationId,
                        principalSchema: "vital",
                        principalTable: "StudyMetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyMain_StudyMetaInformationId",
                schema: "vital",
                table: "StudyMain",
                column: "StudyMetaInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyMetas_PatientId",
                schema: "vital",
                table: "StudyMetas",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyMain",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "StudyMetas",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "vital");
        }
    }
}
