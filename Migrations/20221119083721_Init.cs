using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelProject.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Cars",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                LicencePlate = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FuelType = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cars", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                FirstName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Surname = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UserName = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Password = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "CarUser",
            columns: table => new
            {
                CarsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UsersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CarUser", x => new { x.CarsId, x.UsersId });
                table.ForeignKey(
                    name: "FK_CarUser_Cars_CarsId",
                    column: x => x.CarsId,
                    principalTable: "Cars",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CarUser_Users_UsersId",
                    column: x => x.UsersId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "FuelRecords",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                NameOfFuelStation = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FuelAmount = table.Column<float>(type: "float", nullable: false),
                DashboardKms = table.Column<int>(type: "int", nullable: false),
                PricePerLiter = table.Column<float>(type: "float", nullable: false),
                TotalPrice = table.Column<float>(type: "float", nullable: false),
                CarId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FuelRecords", x => x.Id);
                table.ForeignKey(
                    name: "FK_FuelRecords_Cars_CarId",
                    column: x => x.CarId,
                    principalTable: "Cars",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FuelRecords_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_CarUser_UsersId",
            table: "CarUser",
            column: "UsersId");

        migrationBuilder.CreateIndex(
            name: "IX_FuelRecords_CarId",
            table: "FuelRecords",
            column: "CarId");

        migrationBuilder.CreateIndex(
            name: "IX_FuelRecords_UserId",
            table: "FuelRecords",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CarUser");

        migrationBuilder.DropTable(
            name: "FuelRecords");

        migrationBuilder.DropTable(
            name: "Cars");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
