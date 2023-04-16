using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TurkishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Englishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedWord = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Englishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turkishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedWord = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turkishes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoints = table.Column<float>(type: "real", nullable: false),
                    RequiredExperiencePoints = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEnglish",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    EnglishesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEnglish", x => new { x.CategoriesId, x.EnglishesId });
                    table.ForeignKey(
                        name: "FK_CategoryEnglish_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryEnglish_Englishes_EnglishesId",
                        column: x => x.EnglishesId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTurkish",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    TurkishesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTurkish", x => new { x.CategoriesId, x.TurkishesId });
                    table.ForeignKey(
                        name: "FK_CategoryTurkish_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTurkish_Turkishes_TurkishesId",
                        column: x => x.TurkishesId,
                        principalTable: "Turkishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    LastCorrectAnswerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WrongAnswersCount = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswersCount = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWord_Englishes_WordId",
                        column: x => x.WordId,
                        principalTable: "Englishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWord_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEnglish_EnglishesId",
                table: "CategoryEnglish",
                column: "EnglishesId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTurkish_TurkishesId",
                table: "CategoryTurkish",
                column: "TurkishesId");

            migrationBuilder.CreateIndex(
                name: "IX_Englishes_NormalizedWord",
                table: "Englishes",
                column: "NormalizedWord",
                unique: true,
                filter: "[NormalizedWord] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EnglishTurkishTranslations_TranslationsId1",
                table: "EnglishTurkishTranslations",
                column: "TranslationsId1");

            migrationBuilder.CreateIndex(
                name: "IX_Turkishes_NormalizedWord",
                table: "Turkishes",
                column: "NormalizedWord",
                unique: true,
                filter: "[NormalizedWord] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_UserId1",
                table: "UserWord",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_WordId",
                table: "UserWord",
                column: "WordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryEnglish");

            migrationBuilder.DropTable(
                name: "CategoryTurkish");

            migrationBuilder.DropTable(
                name: "EnglishTurkishTranslations");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserWord");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Turkishes");

            migrationBuilder.DropTable(
                name: "Englishes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
