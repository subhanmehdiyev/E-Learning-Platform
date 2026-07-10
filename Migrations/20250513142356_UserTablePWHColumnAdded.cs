using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_Platform.Migrations
{
    /// <inheritdoc />
    public partial class UserTablePWHColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordWithoutHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordWithoutHash",
                table: "AspNetUsers");
        }
    }
}
