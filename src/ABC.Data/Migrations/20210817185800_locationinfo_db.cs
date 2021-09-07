using Microsoft.EntityFrameworkCore.Migrations;

namespace ABC.Data.Migrations
{
    public partial class locationinfo_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Votes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Votes");
        }
    }
}
