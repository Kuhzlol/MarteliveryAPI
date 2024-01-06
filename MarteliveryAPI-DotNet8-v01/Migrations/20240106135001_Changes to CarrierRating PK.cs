using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class ChangestoCarrierRatingPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_carrier_ratings",
                table: "carrier_ratings");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "quotes",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AddColumn<string>(
                name: "carrier_rating_id",
                table: "carrier_ratings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carrier_ratings",
                table: "carrier_ratings",
                column: "carrier_rating_id");

            migrationBuilder.CreateIndex(
                name: "IX_carrier_ratings_delivery_id",
                table: "carrier_ratings",
                column: "delivery_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_carrier_ratings",
                table: "carrier_ratings");

            migrationBuilder.DropIndex(
                name: "IX_carrier_ratings_delivery_id",
                table: "carrier_ratings");

            migrationBuilder.DropColumn(
                name: "carrier_rating_id",
                table: "carrier_ratings");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "quotes",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_carrier_ratings",
                table: "carrier_ratings",
                columns: new[] { "delivery_id", "customer_id" });
        }
    }
}
