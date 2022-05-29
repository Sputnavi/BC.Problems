using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BC.Problems.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Addresses",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AddressLine1 = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                AddressLine2 = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Addresses", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Problems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BicycleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                BicycleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BicycleSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Place = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateFinished = table.Column<DateTime>(type: "datetime2", nullable: false),
                Stage = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "New"),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Problems", x => x.Id);
                table.ForeignKey(
                    name: "FK_Problems_Addresses_AddressId",
                    column: x => x.AddressId,
                    principalTable: "Addresses",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PartModelProblems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PartModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PartModelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Amount = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                PricePerDetail = table.Column<decimal>(type: "decimal(15,2)", precision: 15, scale: 2, nullable: true),
                ProblemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PartModelProblems", x => x.Id);
                table.ForeignKey(
                    name: "FK_PartModelProblems_Problems_ProblemId",
                    column: x => x.ProblemId,
                    principalTable: "Problems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PartModelProblems_ProblemId_PartModelId",
            table: "PartModelProblems",
            columns: new[] { "ProblemId", "PartModelId" },
            unique: true,
            filter: "[PartModelId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Problems_AddressId",
            table: "Problems",
            column: "AddressId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PartModelProblems");

        migrationBuilder.DropTable(
            name: "Problems");

        migrationBuilder.DropTable(
            name: "Addresses");
    }
}
