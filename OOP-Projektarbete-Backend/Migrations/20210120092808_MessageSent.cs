using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOP_Projektarbete_Backend.Migrations
{
    public partial class MessageSent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Sent",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sent",
                table: "Messages");
        }
    }
}
