using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersCanvasSampleMVC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "TemplateId" },
                values: new object[] { "7013145727", "https://picsum.photos/id/1025/600/800", "Test Product 1", 14.99f, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "TemplateId" },
                values: new object[] { "4245545145", "https://picsum.photos/id/1026/600/800", "Test Product 2", 9.99f, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "TemplateId" },
                values: new object[] { "1253932525", "https://picsum.photos/id/1027/600/800", "Test Product 3", 19.99f, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "TemplateId" },
                values: new object[] { "9559586705", "https://picsum.photos/id/1028/600/800", "Test Product 4", 4.99f, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "TemplateId" },
                values: new object[] { "1540922619", "https://picsum.photos/id/1029/600/800", "Test Product 5", 1.99f, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
