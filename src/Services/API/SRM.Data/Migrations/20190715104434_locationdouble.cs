using Microsoft.EntityFrameworkCore.Migrations;

namespace SRM.Data.Migrations
{
    public partial class locationdouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LocationX",
                table: "Location",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LocationY",
                table: "Location",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationX",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LocationY",
                table: "Location");
        }
    }
}
