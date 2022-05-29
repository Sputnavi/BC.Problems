using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BC.Problems.Migrations
{
    public partial class MakeAddressAsProblemProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Addresses_AddressId",
                table: "Problems");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Problems_AddressId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Problems");

            migrationBuilder.AddColumn<string>(
                name: "Address_AddressLine1",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_AddressLine2",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_AddressLine1",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Address_AddressLine2",
                table: "Problems");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Problems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_Problems_AddressId",
                table: "Problems",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Addresses_AddressId",
                table: "Problems",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
