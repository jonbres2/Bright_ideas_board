using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpExam2.Migrations
{
    public partial class MovedLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Idea",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Idea");

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
