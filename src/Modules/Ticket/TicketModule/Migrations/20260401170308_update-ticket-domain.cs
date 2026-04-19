using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketModule.Migrations
{
    /// <inheritdoc />
    public partial class updateticketdomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WonerFullName",
                table: "Tickets",
                newName: "OwnerFullName");

            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "Messages",
                newName: "OwnerFullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OwnerFullName",
                table: "Tickets",
                newName: "WonerFullName");

            migrationBuilder.RenameColumn(
                name: "OwnerFullName",
                table: "Messages",
                newName: "UserFullName");
        }
    }
}
