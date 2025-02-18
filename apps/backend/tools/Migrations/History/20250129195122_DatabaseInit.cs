using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FwksLabs.ResumeApp.Migrations.History
{
    /// <inheritdoc />
    public partial class DatabaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.CreateTable(
                name: "Resumes",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Handle = table.Column<string>(type: "text", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactInformation = table.Column<string>(type: "jsonb", nullable: false),
                    Location = table.Column<string>(type: "jsonb", nullable: false),
                    Name = table.Column<string>(type: "jsonb", nullable: false),
                    Socials = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationRecords",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Organization = table.Column<string>(type: "text", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: true),
                    FieldOfStudy = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "jsonb", nullable: true),
                    Period = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "App",
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceRecords",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "jsonb", nullable: true),
                    Period = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "App",
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "App",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ResumeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalSchema: "App",
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationRecords_ResumeId",
                schema: "App",
                table: "EducationRecords",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_UK_EducationRecords",
                schema: "App",
                table: "EducationRecords",
                column: "ReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceRecords_ResumeId",
                schema: "App",
                table: "ExperienceRecords",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_UK_ExperienceRecords",
                schema: "App",
                table: "ExperienceRecords",
                column: "ReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_Handle",
                schema: "App",
                table: "Resumes",
                column: "Handle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UK_Resumes",
                schema: "App",
                table: "Resumes",
                column: "ReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                schema: "App",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ResumeId",
                schema: "App",
                table: "Skills",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_UK_Skills",
                schema: "App",
                table: "Skills",
                column: "ReferenceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationRecords",
                schema: "App");

            migrationBuilder.DropTable(
                name: "ExperienceRecords",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "App");

            migrationBuilder.DropTable(
                name: "Resumes",
                schema: "App");
        }
    }
}
