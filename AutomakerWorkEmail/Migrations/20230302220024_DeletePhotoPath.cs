using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomakerWorkEmail.Migrations
{
    public partial class DeletePhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Client");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Worker",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Client",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
