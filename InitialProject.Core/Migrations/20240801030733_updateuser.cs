using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Core.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_ApplicationUserId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                schema: "dbo",
                table: "Users",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ProfileId",
                schema: "dbo",
                table: "Users",
                column: "ProfileId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ProfileId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_ApplicationUserId",
                table: "Images",
                column: "ApplicationUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
