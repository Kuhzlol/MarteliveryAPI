using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class Modificationtocustomertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hashed_password",
                table: "customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pickup_time",
                table: "deliveries",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_time",
                table: "deliveries",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "delivery_status",
                table: "deliveries",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on",
                table: "customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "customers",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_on",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "password",
                table: "customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pickup_time",
                table: "deliveries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_time",
                table: "deliveries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "delivery_status",
                table: "deliveries",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hashed_password",
                table: "customers",
                type: "varchar(250)",
                nullable: true);
        }
    }
}
