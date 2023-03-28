using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedCo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NormalizedWord",
                table: "Turkishes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedWord",
                table: "Englishes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turkishes_NormalizedWord",
                table: "Turkishes",
                column: "NormalizedWord",
                unique: true,
                filter: "[NormalizedWord] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Englishes_NormalizedWord",
                table: "Englishes",
                column: "NormalizedWord",
                unique: true,
                filter: "[NormalizedWord] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Turkishes_NormalizedWord",
                table: "Turkishes");

            migrationBuilder.DropIndex(
                name: "IX_Englishes_NormalizedWord",
                table: "Englishes");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedWord",
                table: "Turkishes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedWord",
                table: "Englishes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
