using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class cateogry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnglishTurkish");

            migrationBuilder.CreateTable(
                name: "EnglishTranslation",
                columns: table => new
                {
                    EnglishId = table.Column<int>(type: "int", nullable: false),
                    TurkishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishTranslation", x => new { x.EnglishId, x.TurkishId });
                    table.ForeignKey(
                        name: "FK_EnglishTranslation_Englishes_EnglishId",
                        column: x => x.EnglishId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnglishTranslation_Turkishes_TurkishId",
                        column: x => x.TurkishId,
                        principalTable: "Turkishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurkishTranslation",
                columns: table => new
                {
                    TurkishId = table.Column<int>(type: "int", nullable: false),
                    EnglishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurkishTranslation", x => new { x.TurkishId, x.EnglishId });
                    table.ForeignKey(
                        name: "FK_TurkishTranslation_Englishes_EnglishId",
                        column: x => x.EnglishId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurkishTranslation_Turkishes_TurkishId",
                        column: x => x.TurkishId,
                        principalTable: "Turkishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnglishTranslation_TurkishId",
                table: "EnglishTranslation",
                column: "TurkishId");

            migrationBuilder.CreateIndex(
                name: "IX_TurkishTranslation_EnglishId",
                table: "TurkishTranslation",
                column: "EnglishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnglishTranslation");

            migrationBuilder.DropTable(
                name: "TurkishTranslation");

            migrationBuilder.CreateTable(
                name: "EnglishTurkish",
                columns: table => new
                {
                    TranslationsId = table.Column<int>(type: "int", nullable: false),
                    TranslationsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishTurkish", x => new { x.TranslationsId, x.TranslationsId1 });
                    table.ForeignKey(
                        name: "FK_EnglishTurkish_Englishes_TranslationsId",
                        column: x => x.TranslationsId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnglishTurkish_Turkishes_TranslationsId1",
                        column: x => x.TranslationsId1,
                        principalTable: "Turkishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnglishTurkish_TranslationsId1",
                table: "EnglishTurkish",
                column: "TranslationsId1");
        }
    }
}
