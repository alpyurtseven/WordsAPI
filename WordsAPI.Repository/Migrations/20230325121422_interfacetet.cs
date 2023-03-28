using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class interfacetet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnglishTranslation");

            migrationBuilder.DropTable(
                name: "TurkishTranslation");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
