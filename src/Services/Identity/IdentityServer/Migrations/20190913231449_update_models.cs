using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations
{
    public partial class update_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AuthenticationEntity");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserSession",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "UserInRole",
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
                table: "Company",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CorporationId",
                table: "AuthenticationEntity",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "AuthenticationEntity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserSession");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserInRole");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "AuthenticationEntity");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AuthenticationEntity");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AuthenticationEntity",
                nullable: false,
                defaultValue: 0);
        }
    }
}
