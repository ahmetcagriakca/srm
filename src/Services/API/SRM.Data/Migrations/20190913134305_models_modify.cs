using Microsoft.EntityFrameworkCore.Migrations;

namespace SRM.Data.Migrations
{
    public partial class models_modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Corporation");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserInRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserActivityLog",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<long>(
                name: "CorporationId",
                table: "User",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentService",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentReport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentPhoneCall",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentOperationLocation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentObstacleType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentLessonSession",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentLesson",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentInstructorRelation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentContact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentAvailableTime",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "StudentAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleStudentTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleStudentOperationAdvice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleStudentOperation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleStudentOperasionLessonRelation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleOperation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "RolePermission",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Role",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "PageRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Page",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ObstacleType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Neighborhood",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "LocationRegionRelation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "LocationRegion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Location",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "LessonSession",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Lesson",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "InstructorBranch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "InstructorAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Instructor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "HospitalAppointmentInstitution",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Hospital",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Document",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "DateCombination",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "County",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Corporation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "City",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Branch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AuthenticationEntity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AppointmentStudentHospitalRelation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AppointmentStudent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ApplicationParameter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Address",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User",
                column: "CorporationId",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserInRole");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserActivityLog");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentService");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentReport");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentPhoneCall");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentOperationLocation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentObstacleType");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentLessonSession");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentLesson");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentInstructorRelation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentContact");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentAvailableTime");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "StudentAddress");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleStudentTemplate");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleStudentOperationAdvice");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleStudentOperation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleStudentOperasionLessonRelation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleOperation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "PageRole");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Page");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ObstacleType");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Neighborhood");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "LocationRegionRelation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "LocationRegion");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "LessonSession");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "InstructorBranch");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "InstructorAddress");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "HospitalAppointmentInstitution");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Hospital");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "DateCombination");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "County");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Corporation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "City");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AuthenticationEntity");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AppointmentStudentHospitalRelation");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AppointmentStudent");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ApplicationParameter");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Address");

            migrationBuilder.AlterColumn<long>(
                name: "CorporationId",
                table: "User",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CorporationId",
                table: "Page",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CorporationId",
                table: "Corporation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User",
                column: "CorporationId",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
