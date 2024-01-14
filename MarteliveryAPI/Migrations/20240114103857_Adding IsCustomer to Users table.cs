using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsCustomertoUserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "users");
        }
    }
}
