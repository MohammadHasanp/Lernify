using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditNameTableCourseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                schema: "course",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "course",
                newName: "Categories",
                newSchema: "course");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Slug",
                schema: "course",
                table: "Categories",
                newName: "IX_Categories_Slug");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "course",
                table: "Categories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "course",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "course",
                newName: "Category",
                newSchema: "course");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Slug",
                schema: "course",
                table: "Category",
                newName: "IX_Category_Slug");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                schema: "course",
                table: "Category",
                column: "Id");
        }
    }
}
