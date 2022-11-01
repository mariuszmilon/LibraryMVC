using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMVC.Migrations
{
    public partial class DeleteCountryFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
