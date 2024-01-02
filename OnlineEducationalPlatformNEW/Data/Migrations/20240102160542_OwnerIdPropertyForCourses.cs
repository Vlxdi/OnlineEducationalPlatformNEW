using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEducationalPlatformNEW.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerIdPropertyForCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Courses");
        }
    }
}
