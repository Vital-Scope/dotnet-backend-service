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
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    PregnancyWeek = table.Column<int>(type: "integer", nullable: true),
                    PregnancyNumber = table.Column<int>(type: "integer", nullable: true),
                    Anamnesis = table.Column<string>(type: "text", nullable: true),
                    DoctorNotes = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyMetas",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Ph = table.Column<float>(type: "real", nullable: true),
                    Glu = table.Column<float>(type: "real", nullable: true),
                    СarbonDioxide = table.Column<float>(type: "real", nullable: true),
                    Be = table.Column<float>(type: "real", nullable: true),
                    Lac = table.Column<float>(type: "real", nullable: true),
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
                name: "UserSettings",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HeartRateMin = table.Column<double>(type: "double precision", nullable: false),
                    HeartRateMax = table.Column<double>(type: "double precision", nullable: false),
                    ContractionMin = table.Column<double>(type: "double precision", nullable: false),
                    ContractionMax = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "vital",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyMain",
                schema: "vital",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<double>(type: "double precision", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                schema: "vital",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyMain",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "UserSettings",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "StudyMetas",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "vital");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "vital");
        }
    }
}
