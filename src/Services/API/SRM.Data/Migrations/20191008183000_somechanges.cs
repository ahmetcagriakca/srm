using Microsoft.EntityFrameworkCore.Migrations;

namespace SRM.Data.Migrations
{
    public partial class somechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "UserInRole",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "UserActivityLog",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "User",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CorporationId",
                table: "User",
                newName: "IX_User_CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentService",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentReportDocument",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentReport",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentPhoneCall",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentOperationLocation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentObstacleType",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentLessonSession",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentLesson",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentInstructorRelation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentContact",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentAvailableTime",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "StudentAddress",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Student",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleTemplate",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleStudentTemplate",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperationAdvice",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleStudentOperasionLessonRelation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ShuttleOperation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "RolePermission",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Role",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "PageRole",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ObstacleType",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Neighborhood",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "LocationRegionRelation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "LocationRegion",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Location",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "LessonSession",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "LessonContentDocument",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Lesson",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "InstructorBranch",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "InstructorAddress",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Instructor",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "HospitalAppointmentInstitution",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Hospital",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Document",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "DateCombination",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "County",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "City",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Branch",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "AuthenticationEntity",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "AppointmentStudentHospitalRelation",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "AppointmentStudent",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "ApplicationParameter",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "CorporationId",
                table: "Address",
                newName: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CompanyId",
                table: "User",
                column: "CompanyId",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CompanyId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "UserInRole",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "UserActivityLog",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "User",
                newName: "CorporationId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CompanyId",
                table: "User",
                newName: "IX_User_CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentService",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentReportDocument",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentReport",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentPhoneCall",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentOperationLocation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentObstacleType",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentLessonSession",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentLesson",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentInstructorRelation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentContact",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentAvailableTime",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "StudentAddress",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Student",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleTemplate",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleStudentTemplate",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleStudentOperationAdvice",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleStudentOperation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleStudentOperasionLessonRelation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ShuttleOperation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "RolePermission",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Role",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "PageRole",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ObstacleType",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Neighborhood",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "LocationRegionRelation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "LocationRegion",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Location",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "LessonSession",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "LessonContentDocument",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Lesson",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "InstructorBranch",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "InstructorAddress",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Instructor",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "HospitalAppointmentInstitution",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Hospital",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Document",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "DateCombination",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "County",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "City",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Branch",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AuthenticationEntity",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AppointmentStudentHospitalRelation",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "AppointmentStudent",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ApplicationParameter",
                newName: "CorporationId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Address",
                newName: "CorporationId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User",
                column: "CorporationId",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
