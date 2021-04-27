using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpExam2.Migrations
{
    public partial class context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Idea_User_UserID",
                table: "Idea");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Idea_IdeaID",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_User_UserID",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Idea",
                table: "Idea");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "Idea",
                newName: "Ideas");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserID",
                table: "Likes",
                newName: "IX_Likes_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Like_IdeaID",
                table: "Likes",
                newName: "IX_Likes_IdeaID");

            migrationBuilder.RenameIndex(
                name: "IX_Idea_UserID",
                table: "Ideas",
                newName: "IX_Ideas_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "LikeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas",
                column: "IdeaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserID",
                table: "Ideas",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Ideas_IdeaID",
                table: "Likes",
                column: "IdeaID",
                principalTable: "Ideas",
                principalColumn: "IdeaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserID",
                table: "Likes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_UserID",
                table: "Ideas");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Ideas_IdeaID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserID",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "Ideas",
                newName: "Idea");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserID",
                table: "Like",
                newName: "IX_Like_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_IdeaID",
                table: "Like",
                newName: "IX_Like_IdeaID");

            migrationBuilder.RenameIndex(
                name: "IX_Ideas_UserID",
                table: "Idea",
                newName: "IX_Idea_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "LikeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Idea",
                table: "Idea",
                column: "IdeaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Idea_User_UserID",
                table: "Idea",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Idea_IdeaID",
                table: "Like",
                column: "IdeaID",
                principalTable: "Idea",
                principalColumn: "IdeaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_User_UserID",
                table: "Like",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
