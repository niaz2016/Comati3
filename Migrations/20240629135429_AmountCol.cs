using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comati3.Migrations
{
    /// <inheritdoc />
    public partial class AmountCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Members",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Members");
        }
    }
}
