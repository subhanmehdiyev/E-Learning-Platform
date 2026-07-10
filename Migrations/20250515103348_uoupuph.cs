using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_Platform.Migrations
{
    /// <inheritdoc />
    public partial class uoupuph : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseAuthorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quiz1Result = table.Column<double>(type: "float", nullable: false),
                    MidtermResult = table.Column<double>(type: "float", nullable: false),
                    Quiz2Result = table.Column<double>(type: "float", nullable: false),
                    FinalResult = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseResults");
        }
    }
}
