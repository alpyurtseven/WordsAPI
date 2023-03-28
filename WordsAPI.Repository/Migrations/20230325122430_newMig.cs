using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class newMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Englishes_Englishes_EnglishId",
                table: "Englishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Turkishes_Turkishes_TurkishId",
                table: "Turkishes");

            migrationBuilder.DropIndex(
                name: "IX_Turkishes_TurkishId",
                table: "Turkishes");

            migrationBuilder.DropIndex(
                name: "IX_Englishes_EnglishId",
                table: "Englishes");

            migrationBuilder.DropColumn(
                name: "TurkishId",
                table: "Turkishes");

            migrationBuilder.DropColumn(
                name: "EnglishId",
                table: "Englishes");

            migrationBuilder.CreateTable(
                name: "EnglishTurkishTranslations",
                columns: table => new
                {
                    TranslationsId = table.Column<int>(type: "int", nullable: false),
                    TranslationsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishTurkishTranslations", x => new { x.TranslationsId, x.TranslationsId1 });
                    table.ForeignKey(
                        name: "FK_EnglishTurkishTranslations_Englishes_TranslationsId",
                        column: x => x.TranslationsId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnglishTurkishTranslations_Turkishes_TranslationsId1",
                        column: x => x.TranslationsId1,
                        principalTable: "Turkishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnglishTurkishTranslations_TranslationsId1",
                table: "EnglishTurkishTranslations",
                column: "TranslationsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnglishTurkishTranslations");

            migrationBuilder.AddColumn<int>(
                name: "TurkishId",
                table: "Turkishes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnglishId",
                table: "Englishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turkishes_TurkishId",
                table: "Turkishes",
                column: "TurkishId");

            migrationBuilder.CreateIndex(
                name: "IX_Englishes_EnglishId",
                table: "Englishes",
                column: "EnglishId");

            migrationBuilder.AddForeignKey(
                name: "FK_Englishes_Englishes_EnglishId",
                table: "Englishes",
                column: "EnglishId",
                principalTable: "Englishes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turkishes_Turkishes_TurkishId",
                table: "Turkishes",
                column: "TurkishId",
                principalTable: "Turkishes",
                principalColumn: "Id");
        }
    }
}
