using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace SRM.Data.Migrations
{
    public partial class corporation_migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "UserInRole",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "UserActivityLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentReportDocument",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentReport",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentPhoneCall",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentOperationLocation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentObstacleType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentLessonSession",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentLesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentInstructorRelation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentContact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentAvailableTime",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "StudentAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleTemplate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleStudentTemplate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleStudentOperationAdvice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleStudentOperation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleStudentOperasionLessonRelation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ShuttleOperation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "RolePermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Role",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "PageRole",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Page",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ObstacleType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Neighborhood",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "LocationRegionRelation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "LocationRegion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Location",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "LessonSession",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "LessonContentDocument",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Lesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "InstructorBranch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "InstructorAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Instructor",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "HospitalAppointmentInstitution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Hospital",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "DateCombination",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "County",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "City",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Branch",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "AuthenticationEntity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "AppointmentStudentHospitalRelation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "AppointmentStudent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "ApplicationParameter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Address",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Corporation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CorporationId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Corporation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "UserInRole");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "UserActivityLog");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentService");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentReportDocument");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentReport");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentPhoneCall");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentOperationLocation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentObstacleType");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentLessonSession");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentLesson");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentInstructorRelation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentContact");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentAvailableTime");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "StudentAddress");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleTemplate");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleStudentTemplate");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperationAdvice");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperasionLessonRelation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ShuttleOperation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "PageRole");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ObstacleType");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Neighborhood");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "LocationRegionRelation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "LocationRegion");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "LessonSession");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "LessonContentDocument");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "InstructorBranch");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "InstructorAddress");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "HospitalAppointmentInstitution");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "DateCombination");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "County");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "AuthenticationEntity");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "AppointmentStudentHospitalRelation");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "AppointmentStudent");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "ApplicationParameter");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Address");
        }
    }
}
