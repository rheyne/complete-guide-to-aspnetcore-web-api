using Microsoft.EntityFrameworkCore.Migrations;

namespace my_books.Migrations
{
    // Created via NuGet-Paket-Manager -> Paket-Manager-Konsole -> "Add-Migration BookAuthorColumnRemoved"
    // Executed via NuGet-Paket-Manager -> Paket-Manager-Konsole -> "Update-Database"
    public partial class BookAuthorColumnRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
