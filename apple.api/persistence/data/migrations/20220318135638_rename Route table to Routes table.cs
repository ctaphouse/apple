using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apple.api.persistence.data.migrations
{
    public partial class renameRoutetabletoRoutestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Route_Trails_TrailId",
                table: "Route");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameIndex(
                name: "IX_Route_TrailId",
                table: "Routes",
                newName: "IX_Routes_TrailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Trails_TrailId",
                table: "Routes",
                column: "TrailId",
                principalTable: "Trails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Trails_TrailId",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_TrailId",
                table: "Route",
                newName: "IX_Route_TrailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Trails_TrailId",
                table: "Route",
                column: "TrailId",
                principalTable: "Trails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
