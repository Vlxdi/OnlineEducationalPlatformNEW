using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEducationalPlatformNEW.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserClassPropAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "AspNetUsers");
        }
    }
}
