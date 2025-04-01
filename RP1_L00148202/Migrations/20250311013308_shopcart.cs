using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RP1_L00148202.Migrations
{
    /// <inheritdoc />
    public partial class shopcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Products",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
