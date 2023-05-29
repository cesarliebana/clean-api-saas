using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaaV.SaaS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Auditory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Dummy",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                table: "Dummy",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserId",
                table: "Dummy",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                table: "Dummy",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Dummy");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                table: "Dummy");

            migrationBuilder.DropColumn(
                name: "ModifiedUserId",
                table: "Dummy");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                table: "Dummy");
        }
    }
}
