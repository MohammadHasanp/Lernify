using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseActionStatus",
                schema: "dbo",
                table: "Courses",
                newName: "ActionStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActionStatus",
                schema: "dbo",
                table: "Courses",
                newName: "CourseActionStatus");
        }
    }
}
