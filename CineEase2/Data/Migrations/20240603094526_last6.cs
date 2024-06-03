using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineEase2.Data.Migrations
{
    /// <inheritdoc />
    public partial class last6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Ticket",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Ticket");
        }
    }
}
