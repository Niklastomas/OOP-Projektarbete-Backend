using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_Projektarbete_Backend.Migrations
{
    public partial class UsersMovies_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_UserId1",
                table: "UsersMovies");

            migrationBuilder.DropIndex(
                name: "IX_UsersMovies_UserId1",
                table: "UsersMovies");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersMovies");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UsersMovies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_UserId1",
                table: "UsersMovies",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_UserId1",
                table: "UsersMovies",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
