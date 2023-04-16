using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordsAPI.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UserWordRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWord_Users_UserId1",
                table: "UserWord");

            migrationBuilder.DropIndex(
                name: "IX_UserWord_UserId1",
                table: "UserWord");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserWord");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserWord",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_UserId",
                table: "UserWord",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWord_Users_UserId",
                table: "UserWord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWord_Users_UserId",
                table: "UserWord");

            migrationBuilder.DropIndex(
                name: "IX_UserWord_UserId",
                table: "UserWord");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserWord",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserWord",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_UserId1",
                table: "UserWord",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWord_Users_UserId1",
                table: "UserWord",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
