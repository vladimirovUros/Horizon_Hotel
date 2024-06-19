using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MainImageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImagePath",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "MainImageId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_MainImageId",
                table: "Rooms",
                column: "MainImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Images_MainImageId",
                table: "Rooms",
                column: "MainImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Images_MainImageId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_MainImageId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MainImageId",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "MainImagePath",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
