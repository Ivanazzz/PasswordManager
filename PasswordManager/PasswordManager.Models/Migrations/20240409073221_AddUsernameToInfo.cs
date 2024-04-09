using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Models.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Infos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Infos");
        }
    }
}
