using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLogger.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentificationNemberToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserIdentificationNumber",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserIdentificationNumber",
                table: "Users",
                column: "UserIdentificationNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserIdentificationNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserIdentificationNumber",
                table: "Users");
        }
    }
}
