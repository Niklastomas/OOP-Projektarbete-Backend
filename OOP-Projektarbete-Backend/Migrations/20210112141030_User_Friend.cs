using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_Projektarbete_Backend.Migrations
{
    public partial class User_Friend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestedToId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FriendRequestFlag = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_AspNetUsers_RequestedToId",
                        column: x => x.RequestedToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friend_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_RequestedById",
                table: "Friend",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_RequestedToId",
                table: "Friend",
                column: "RequestedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_UserId",
                table: "Friend",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");
        }
    }
}
