using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationGuide.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectricityCount",
                columns: table => new
                {
                    ElectricityCountId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityCount", x => x.ElectricityCountId);
                });

            migrationBuilder.CreateTable(
                name: "ElectricityValues",
                columns: table => new
                {
                    ElectricityValuesId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ElectricityCountForeignKey = table.Column<long>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    ActiveReceive = table.Column<float>(nullable: false),
                    ActiveOutput = table.Column<float>(nullable: false),
                    ReactiveReceive = table.Column<float>(nullable: false),
                    ReactiveOutput = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityValues", x => x.ElectricityValuesId);
                    table.ForeignKey(
                        name: "FK_ElectricityValues_ElectricityCount_ElectricityCountForeignKey",
                        column: x => x.ElectricityCountForeignKey,
                        principalTable: "ElectricityCount",
                        principalColumn: "ElectricityCountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityCount_SerialNumber",
                table: "ElectricityCount",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityValues_ElectricityCountForeignKey",
                table: "ElectricityValues",
                column: "ElectricityCountForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricityValues");

            migrationBuilder.DropTable(
                name: "ElectricityCount");
        }
    }
}
