using Microsoft.EntityFrameworkCore.Migrations;

namespace SRM.Data.Migrations
{
    public partial class templete_Delete_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShuttleTemplate",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShuttleTemplate");
        }
    }
}
