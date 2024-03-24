using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasswordManager.Models.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueConstraintFromInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Infos_Website",
                table: "Infos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Infos_Website",
                table: "Infos",
                column: "Website",
                unique: true);
        }
    }
}
