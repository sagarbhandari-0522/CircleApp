using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Navigation_Property_In_FriendRequest_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Friendrequests_ReceiverId",
                table: "Friendrequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendrequests_SenderId",
                table: "Friendrequests",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendrequests_Users_ReceiverId",
                table: "Friendrequests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendrequests_Users_SenderId",
                table: "Friendrequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendrequests_Users_ReceiverId",
                table: "Friendrequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendrequests_Users_SenderId",
                table: "Friendrequests");

            migrationBuilder.DropIndex(
                name: "IX_Friendrequests_ReceiverId",
                table: "Friendrequests");

            migrationBuilder.DropIndex(
                name: "IX_Friendrequests_SenderId",
                table: "Friendrequests");
        }
    }
}
