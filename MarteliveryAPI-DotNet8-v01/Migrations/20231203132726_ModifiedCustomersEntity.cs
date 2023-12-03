using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCustomersEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login_provider",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "provider_key",
                table: "Customers",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "mobile",
                table: "Customers",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                table: "Customers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Customers",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Customers",
                newName: "provider_key");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "Customers",
                newName: "mobile");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "Customers",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Customers",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "login_provider",
                table: "Customers",
                type: "text",
                nullable: true);
        }
    }
}
