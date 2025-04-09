using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class change_type_of_createdat_to_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Users_ReceiverId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Users_SenderId",
                table: "Friendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship");

            migrationBuilder.RenameTable(
                name: "Friendship",
                newName: "Friendships");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_SenderId",
                table: "Friendships",
                newName: "IX_Friendships_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_ReceiverId",
                table: "Friendships",
                newName: "IX_Friendships_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Friendrequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendrequests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_ReceiverId",
                table: "Friendships",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_SenderId",
                table: "Friendships",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_ReceiverId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_SenderId",
                table: "Friendships");

            migrationBuilder.DropTable(
                name: "Friendrequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "Friendship");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_SenderId",
                table: "Friendship",
                newName: "IX_Friendship_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_ReceiverId",
                table: "Friendship",
                newName: "IX_Friendship_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Users_ReceiverId",
                table: "Friendship",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Users_SenderId",
                table: "Friendship",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
