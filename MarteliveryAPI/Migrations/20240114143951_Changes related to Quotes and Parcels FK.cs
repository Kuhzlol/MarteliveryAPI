using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesrelatedtoQuotesandParcelsFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carrier_ratings_customers_customer_id",
                table: "carrier_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_parcels_customers_customer_id",
                table: "parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_quotes_carriers_carrier_id",
                table: "quotes");

            migrationBuilder.RenameColumn(
                name: "carrier_id",
                table: "quotes",
                newName: "CarrierId");

            migrationBuilder.RenameIndex(
                name: "IX_quotes_carrier_id",
                table: "quotes",
                newName: "IX_quotes_CarrierId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "parcels",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_parcels_customer_id",
                table: "parcels",
                newName: "IX_parcels_CustomerId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "carrier_ratings",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_carrier_ratings_customer_id",
                table: "carrier_ratings",
                newName: "IX_carrier_ratings_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "CarrierId",
                table: "quotes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "quotes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "parcels",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "parcels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "carrier_ratings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "carrier_ratings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_quotes_user_id",
                table: "quotes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_parcels_user_id",
                table: "parcels",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_carrier_ratings_user_id",
                table: "carrier_ratings",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_carrier_ratings_customers_CustomerId",
                table: "carrier_ratings",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_carrier_ratings_users_user_id",
                table: "carrier_ratings",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parcels_customers_CustomerId",
                table: "parcels",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_parcels_users_user_id",
                table: "parcels",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quotes_carriers_CarrierId",
                table: "quotes",
                column: "CarrierId",
                principalTable: "carriers",
                principalColumn: "carrier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_quotes_users_user_id",
                table: "quotes",
                column: "user_id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carrier_ratings_customers_CustomerId",
                table: "carrier_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_carrier_ratings_users_user_id",
                table: "carrier_ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_parcels_customers_CustomerId",
                table: "parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_parcels_users_user_id",
                table: "parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_quotes_carriers_CarrierId",
                table: "quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_quotes_users_user_id",
                table: "quotes");

            migrationBuilder.DropIndex(
                name: "IX_quotes_user_id",
                table: "quotes");

            migrationBuilder.DropIndex(
                name: "IX_parcels_user_id",
                table: "parcels");

            migrationBuilder.DropIndex(
                name: "IX_carrier_ratings_user_id",
                table: "carrier_ratings");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "quotes");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "parcels");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "carrier_ratings");

            migrationBuilder.RenameColumn(
                name: "CarrierId",
                table: "quotes",
                newName: "carrier_id");

            migrationBuilder.RenameIndex(
                name: "IX_quotes_CarrierId",
                table: "quotes",
                newName: "IX_quotes_carrier_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "parcels",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_parcels_CustomerId",
                table: "parcels",
                newName: "IX_parcels_customer_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "carrier_ratings",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_carrier_ratings_CustomerId",
                table: "carrier_ratings",
                newName: "IX_carrier_ratings_customer_id");

            migrationBuilder.AlterColumn<string>(
                name: "carrier_id",
                table: "quotes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                table: "parcels",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "customer_id",
                table: "carrier_ratings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_carrier_ratings_customers_customer_id",
                table: "carrier_ratings",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parcels_customers_customer_id",
                table: "parcels",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quotes_carriers_carrier_id",
                table: "quotes",
                column: "carrier_id",
                principalTable: "carriers",
                principalColumn: "carrier_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
