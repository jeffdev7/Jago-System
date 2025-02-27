using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jago.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Passengers",
                newName: "DocumentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentNumber",
                table: "Passengers",
                newName: "RG");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
