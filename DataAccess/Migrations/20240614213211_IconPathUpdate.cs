using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IconPathUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_IconId",
                table: "Services",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Images_IconId",
                table: "Services",
                column: "IconId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Images_IconId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_IconId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
