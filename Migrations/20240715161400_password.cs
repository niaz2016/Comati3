using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comati3.Migrations
{
    /// <inheritdoc />
    public partial class password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComatiMemberNo",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Person",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Person",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComatiMemberNo",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
