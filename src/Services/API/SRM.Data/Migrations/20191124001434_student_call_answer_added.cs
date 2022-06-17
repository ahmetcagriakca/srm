using Microsoft.EntityFrameworkCore.Migrations;

namespace SRM.Data.Migrations
{
    public partial class student_call_answer_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPhoneCall_ShuttleStudentOperationAdvice_ShuttleStude~",
                table: "StudentPhoneCall");

            migrationBuilder.DropIndex(
                name: "IX_StudentPhoneCall_ShuttleStudentOperationAdviceId",
                table: "StudentPhoneCall");

            migrationBuilder.DropColumn(
                name: "ShuttleStudentOperationAdviceId",
                table: "StudentPhoneCall");

            migrationBuilder.AddColumn<int>(
                name: "StudentAnswer",
                table: "StudentPhoneCall",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentAnswer",
                table: "StudentPhoneCall");

            migrationBuilder.AddColumn<long>(
                name: "ShuttleStudentOperationAdviceId",
                table: "StudentPhoneCall",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentPhoneCall_ShuttleStudentOperationAdviceId",
                table: "StudentPhoneCall",
                column: "ShuttleStudentOperationAdviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPhoneCall_ShuttleStudentOperationAdvice_ShuttleStude~",
                table: "StudentPhoneCall",
                column: "ShuttleStudentOperationAdviceId",
                principalTable: "ShuttleStudentOperationAdvice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
