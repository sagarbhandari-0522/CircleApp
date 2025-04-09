using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class change_ID_to_Id_Friendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Friendships",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Friendships",
                newName: "ID");
        }
    }
}
