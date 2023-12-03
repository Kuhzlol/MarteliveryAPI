using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTablesAndRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    quote_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    delivery_distance = table.Column<float>(type: "real", nullable: false),
                    price_per_km = table.Column<float>(type: "real", nullable: false),
                    total_price = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<string>(type: "varchar(250)", nullable: false),
                    carrier_id = table.Column<int>(type: "integer", nullable: false),
                    parcel_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotes", x => x.quote_id);
                    table.ForeignKey(
                        name: "FK_quotes_carriers_carrier_id",
                        column: x => x.carrier_id,
                        principalTable: "carriers",
                        principalColumn: "carrier_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quotes_parcels_parcel_id",
                        column: x => x.parcel_id,
                        principalTable: "parcels",
                        principalColumn: "parcel_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pickup_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_status = table.Column<string>(type: "varchar(250)", nullable: false),
                    quote_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveries", x => x.delivery_id);
                    table.ForeignKey(
                        name: "FK_deliveries_quotes_quote_id",
                        column: x => x.quote_id,
                        principalTable: "quotes",
                        principalColumn: "quote_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_method = table.Column<string>(type: "varchar(250)", nullable: false),
                    payment_status = table.Column<string>(type: "varchar(250)", nullable: false),
                    payment_amount = table.Column<float>(type: "real", nullable: false),
                    payment_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quote_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_payments_quotes_quote_id",
                        column: x => x.quote_id,
                        principalTable: "quotes",
                        principalColumn: "quote_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carrier_ratings",
                columns: table => new
                {
                    delivery_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    carrier_rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrier_ratings", x => new { x.delivery_id, x.customer_id });
                    table.ForeignKey(
                        name: "FK_carrier_ratings_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carrier_ratings_deliveries_delivery_id",
                        column: x => x.delivery_id,
                        principalTable: "deliveries",
                        principalColumn: "delivery_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carrier_ratings_customer_id",
                table: "carrier_ratings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_quote_id",
                table: "deliveries",
                column: "quote_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_quote_id",
                table: "payments",
                column: "quote_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotes_carrier_id",
                table: "quotes",
                column: "carrier_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotes_parcel_id",
                table: "quotes",
                column: "parcel_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carrier_ratings");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "deliveries");

            migrationBuilder.DropTable(
                name: "quotes");
        }
    }
}
