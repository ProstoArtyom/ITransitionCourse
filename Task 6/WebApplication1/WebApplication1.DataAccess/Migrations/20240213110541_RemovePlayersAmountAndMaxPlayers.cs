using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovePlayersAmountAndMaxPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Board");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Board",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
