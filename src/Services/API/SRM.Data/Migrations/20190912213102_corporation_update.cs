using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SRM.Data.Migrations
{
    public partial class corporation_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CorporationId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CorporationId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CorporationId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Corporation");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Corporation",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "LongName",
                table: "Corporation",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_CorporationId",
                table: "User",
                column: "CorporationId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User",
                column: "CorporationId",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Corporation_CorporationId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CorporationId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LongName",
                table: "Corporation");

            migrationBuilder.AddColumn<int>(
                name: "CorporationId1",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Corporation",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Corporation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CorporationId1",
                table: "User",
                column: "CorporationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Corporation_CorporationId1",
                table: "User",
                column: "CorporationId1",
                principalTable: "Corporation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
