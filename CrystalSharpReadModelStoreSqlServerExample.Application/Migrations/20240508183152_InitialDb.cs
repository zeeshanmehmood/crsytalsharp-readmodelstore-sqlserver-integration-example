using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrystalSharpReadModelStoreSqlServerExample.Application.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GlobalUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrder");
        }
    }
}
