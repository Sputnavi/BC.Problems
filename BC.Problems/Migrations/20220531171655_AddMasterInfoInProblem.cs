using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BC.Problems.Migrations
{
    public partial class AddMasterInfoInProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MasterEmail",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterId",
                table: "Problems",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterEmail",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "Problems");
        }
    }
}
