using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Core.Migrations
{
    /// <inheritdoc />
    public partial class updateUser_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "dbo",
                table: "Users",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "CityId",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "dbo",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Role",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "dbo",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "dbo",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "dbo",
                table: "Users",
                newName: "status");

            migrationBuilder.AlterColumn<string>(
                name: "CityId",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Role",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                schema: "dbo",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
