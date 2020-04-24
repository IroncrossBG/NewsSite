namespace NewsSite.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "Content", "CreatedOn", "ModifiedOn", "Subtitle", "Title", "Views" },
                values: new object[] { 1, "ASP.NET Core", "Корона", new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(8925), new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(9425), null, "Коронавирус", 0 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "Content", "CreatedOn", "ModifiedOn", "Subtitle", "Title", "Views" },
                values: new object[] { 2, "ASP.NET Core", "Бойко", new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(9919), new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(9928), null, "Борисов", 0 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "Content", "CreatedOn", "ModifiedOn", "Subtitle", "Title", "Views" },
                values: new object[] { 3, "ASP.NET Core", "БГ", new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(9934), new DateTime(2020, 4, 1, 18, 39, 33, 672, DateTimeKind.Utc).AddTicks(9935), null, "България", 0 });
        }
    }
}
