using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CircleApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_Id_To_PostId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");
        }
    }
}
