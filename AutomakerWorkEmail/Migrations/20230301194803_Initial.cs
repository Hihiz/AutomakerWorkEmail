using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomakerWorkEmail.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Role",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Role", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Services",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Cost = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Services", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Client",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Passport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        PhotoPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        RoleId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Client", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Client_Role",
            //            column: x => x.RoleId,
            //            principalTable: "Role",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Worker",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        PhotoPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            //        RoleId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Worker", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Worker_Role",
            //            column: x => x.RoleId,
            //            principalTable: "Role",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClientOrder",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        TrackNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        FinalCost = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
            //        DateDispatch = table.Column<DateTime>(type: "datetime", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        ServiceId = table.Column<int>(type: "int", nullable: false),
            //        ClientId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClientOrder", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Order_Client",
            //            column: x => x.ClientId,
            //            principalTable: "Client",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Order_Services",
            //            column: x => x.ServiceId,
            //            principalTable: "Services",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Client_RoleId",
            //    table: "Client",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClientOrder_ClientId",
            //    table: "ClientOrder",
            //    column: "ClientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClientOrder_ServiceId",
            //    table: "ClientOrder",
            //    column: "ServiceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Worker_RoleId",
            //    table: "Worker",
            //    column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientOrder");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
