using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomersCanvasSampleMVC.Migrations
{
    public partial class AddEditorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "EditorType",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorType",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Products",
                type: "INTEGER",
                nullable: true);
        }
    }
}
