using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZeroExample.Data.Migrations
{
    public partial class SubLocationTableAdd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubLocations_Locations_LocationId",
                table: "SubLocations");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "SubLocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SubLocations_Locations_LocationId",
                table: "SubLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubLocations_Locations_LocationId",
                table: "SubLocations");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "SubLocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubLocations_Locations_LocationId",
                table: "SubLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
