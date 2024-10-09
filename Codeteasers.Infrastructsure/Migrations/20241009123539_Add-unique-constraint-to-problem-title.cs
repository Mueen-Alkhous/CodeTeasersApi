using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codeteasers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adduniqueconstrainttoproblemtitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Problems_Title",
                table: "Problems",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Problems_Title",
                table: "Problems");
        }
    }
}
