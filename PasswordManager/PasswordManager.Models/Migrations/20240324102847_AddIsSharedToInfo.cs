using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSharedToInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "Infos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "Infos");
        }
    }
}
