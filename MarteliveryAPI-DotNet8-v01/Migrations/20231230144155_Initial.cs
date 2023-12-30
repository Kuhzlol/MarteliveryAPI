using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carriers",
                columns: table => new
                {
                    carrier_id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    email = table.Column<string>(type: "varchar(250)", nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "boolean", nullable: true),
                    hashed_password = table.Column<string>(type: "varchar(250)", nullable: true),
                    phone_number = table.Column<string>(type: "varchar(250)", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    login_provider = table.Column<string>(type: "varchar(250)", nullable: true),
                    token = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carriers", x => x.carrier_id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    email = table.Column<string>(type: "varchar(250)", nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "boolean", nullable: true),
                    password = table.Column<string>(type: "varchar(250)", nullable: false),
                    phone_number = table.Column<string>(type: "varchar(250)", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    login_provider = table.Column<string>(type: "varchar(250)", nullable: true),
                    token = table.Column<string>(type: "varchar(250)", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "test_users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parcels",
                columns: table => new
                {
                    parcel_id = table.Column<string>(type: "text", nullable: false),
                    pickup_location = table.Column<string>(type: "varchar(250)", nullable: false),
                    delivery_location = table.Column<string>(type: "varchar(250)", nullable: false),
                    length = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    customer_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parcels", x => x.parcel_id);
                    table.ForeignKey(
                        name: "FK_parcels_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    quote_id = table.Column<string>(type: "text", nullable: false),
                    delivery_distance = table.Column<float>(type: "real", nullable: false),
                    price_per_km = table.Column<float>(type: "real", nullable: false),
                    total_price = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<string>(type: "varchar(250)", nullable: false),
                    carrier_id = table.Column<string>(type: "text", nullable: false),
                    parcel_id = table.Column<string>(type: "text", nullable: false)
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
                    delivery_id = table.Column<string>(type: "text", nullable: false),
                    pickup_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    delivery_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    delivery_status = table.Column<string>(type: "varchar(250)", nullable: true),
                    quote_id = table.Column<string>(type: "text", nullable: false)
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
                    payment_id = table.Column<string>(type: "text", nullable: false),
                    payment_method = table.Column<string>(type: "varchar(250)", nullable: false),
                    payment_status = table.Column<string>(type: "varchar(250)", nullable: false),
                    payment_amount = table.Column<float>(type: "real", nullable: false),
                    payment_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quote_id = table.Column<string>(type: "text", nullable: false)
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
                    delivery_id = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<string>(type: "text", nullable: false),
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
                name: "IX_parcels_customer_id",
                table: "parcels",
                column: "customer_id");

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
                name: "test_users");

            migrationBuilder.DropTable(
                name: "deliveries");

            migrationBuilder.DropTable(
                name: "quotes");

            migrationBuilder.DropTable(
                name: "carriers");

            migrationBuilder.DropTable(
                name: "parcels");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
