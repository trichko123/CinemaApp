using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Repository.Migrations
{
    public partial class updateTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MovieGenre",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MovieGenre",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
