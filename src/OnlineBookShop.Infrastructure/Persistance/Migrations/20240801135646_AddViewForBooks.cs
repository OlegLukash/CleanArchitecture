using Microsoft.EntityFrameworkCore.Migrations;
using OnlineBookShop.Infrastructure.Persistance.Extensions;

#nullable disable

namespace OnlineBookShop.Dal.Migrations
{
    public partial class AddViewForBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateViewFromFile("vBooks.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.vBooks");
        }
    }
}
