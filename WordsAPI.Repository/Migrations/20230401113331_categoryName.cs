using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class categoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TurkishName",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TurkishName",
                table: "Categories");
        }
    }
}
