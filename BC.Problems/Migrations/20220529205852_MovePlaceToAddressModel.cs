using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BC.Problems.Migrations
{
    public partial class MovePlaceToAddressModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Problems",
                newName: "Address_Place");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Place",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Place",
                table: "Problems",
                newName: "Place");

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "Problems",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
