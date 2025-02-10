using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEFIdentity.Migrations
{
    /// <inheritdoc />
    public partial class Handle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Handle",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Handle",
                table: "AspNetUsers");
        }
    }
}
