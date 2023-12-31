using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTotalDistancefromquoteandaddedtoParcel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delivery_distance",
                table: "quotes");

            migrationBuilder.AddColumn<float>(
                name: "total_distance",
                table: "parcels",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_distance",
                table: "parcels");

            migrationBuilder.AddColumn<float>(
                name: "delivery_distance",
                table: "quotes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
