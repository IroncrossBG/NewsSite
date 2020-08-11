using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsSite.Data.Migrations
{
    public partial class typofix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "LastRun",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LastRun",
                newName: "id");
        }
    }
}
