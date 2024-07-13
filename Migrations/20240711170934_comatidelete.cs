using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comati3.Migrations
{
    /// <inheritdoc />
    public partial class comatidelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComatiPayments_Comaties_ComatiId",
                table: "ComatiPayments");

            migrationBuilder.AlterColumn<int>(
                name: "ComatiId",
                table: "ComatiPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ComatiPayments_Comaties_ComatiId",
                table: "ComatiPayments",
                column: "ComatiId",
                principalTable: "Comaties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComatiPayments_Comaties_ComatiId",
                table: "ComatiPayments");

            migrationBuilder.AlterColumn<int>(
                name: "ComatiId",
                table: "ComatiPayments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComatiPayments_Comaties_ComatiId",
                table: "ComatiPayments",
                column: "ComatiId",
                principalTable: "Comaties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
