using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerParcelRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carriers",
                columns: table => new
                {
                    carrier_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    email = table.Column<string>(type: "varchar(250)", nullable: false),
                    is_email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    hashed_password = table.Column<string>(type: "varchar(250)", nullable: false),
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
                name: "parcels",
                columns: table => new
                {
                    parcel_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pickup_location = table.Column<string>(type: "varchar(250)", nullable: false),
                    delivery_location = table.Column<string>(type: "varchar(250)", nullable: false),
                    length = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_parcels_customer_id",
                table: "parcels",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carriers");

            migrationBuilder.DropTable(
                name: "parcels");
        }
    }
}
