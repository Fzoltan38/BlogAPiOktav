using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class CorrectPostTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_bloggers_BloggerId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "posts");

            migrationBuilder.RenameIndex(
                name: "IX_Post_BloggerId",
                table: "posts",
                newName: "IX_posts_BloggerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_bloggers_BloggerId",
                table: "posts",
                column: "BloggerId",
                principalTable: "bloggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_bloggers_BloggerId",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_posts_BloggerId",
                table: "Post",
                newName: "IX_Post_BloggerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_bloggers_BloggerId",
                table: "Post",
                column: "BloggerId",
                principalTable: "bloggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
