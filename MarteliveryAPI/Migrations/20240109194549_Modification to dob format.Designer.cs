﻿// <auto-generated />
using System;
using MarteliveryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarteliveryAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240109194549_Modification to dob format")]
    partial class Modificationtodobformat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarteliveryAPI.Models.Carrier", b =>
                {
                    b.Property<string>("CarrierId")
                        .HasColumnType("text")
                        .HasColumnName("carrier_id");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("first_name");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("hashed_password");

                    b.Property<bool?>("IsEmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_confirmed");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("last_name");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("login_provider");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("token");

                    b.HasKey("CarrierId");

                    b.ToTable("carriers");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.CarrierRating", b =>
                {
                    b.Property<string>("CarrierRatingId")
                        .HasColumnType("text")
                        .HasColumnName("carrier_rating_id");

                    b.Property<int>("CarrierRate")
                        .HasColumnType("integer")
                        .HasColumnName("carrier_rate");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_id");

                    b.Property<string>("DeliveryId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("delivery_id");

                    b.HasKey("CarrierRatingId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("carrier_ratings");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("text")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("first_name");

                    b.Property<bool?>("IsEmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_confirmed");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("last_name");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("login_provider");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("token");

                    b.HasKey("CustomerId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Delivery", b =>
                {
                    b.Property<string>("DeliveryId")
                        .HasColumnType("text")
                        .HasColumnName("delivery_id");

                    b.Property<string>("DeliveryStatus")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("delivery_status");

                    b.Property<DateTime?>("DeliveryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delivery_time");

                    b.Property<DateTime?>("PickupTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("pickup_time");

                    b.Property<string>("QuoteId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("quote_id");

                    b.HasKey("DeliveryId");

                    b.HasIndex("QuoteId");

                    b.ToTable("deliveries");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Parcel", b =>
                {
                    b.Property<string>("ParcelId")
                        .HasColumnType("text")
                        .HasColumnName("parcel_id");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("customer_id");

                    b.Property<string>("DeliveryLocation")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("delivery_location");

                    b.Property<float>("Height")
                        .HasColumnType("real")
                        .HasColumnName("height");

                    b.Property<float>("Length")
                        .HasColumnType("real")
                        .HasColumnName("length");

                    b.Property<string>("PickupLocation")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("pickup_location");

                    b.Property<float>("TotalDistance")
                        .HasColumnType("float")
                        .HasColumnName("total_distance");

                    b.Property<float>("Weight")
                        .HasColumnType("real")
                        .HasColumnName("weight");

                    b.Property<float>("Width")
                        .HasColumnType("real")
                        .HasColumnName("width");

                    b.HasKey("ParcelId");

                    b.HasIndex("CustomerId");

                    b.ToTable("parcels");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Payment", b =>
                {
                    b.Property<string>("PaymentId")
                        .HasColumnType("text")
                        .HasColumnName("payment_id");

                    b.Property<float>("PaymentAmount")
                        .HasColumnType("real")
                        .HasColumnName("payment_amount");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("payment_method");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("payment_status");

                    b.Property<DateTime>("PaymentTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("payment_time");

                    b.Property<string>("QuoteId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("quote_id");

                    b.HasKey("PaymentId");

                    b.HasIndex("QuoteId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Quote", b =>
                {
                    b.Property<string>("QuoteId")
                        .HasColumnType("text")
                        .HasColumnName("quote_id");

                    b.Property<string>("CarrierId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("carrier_id");

                    b.Property<string>("ParcelId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("parcel_id");

                    b.Property<float>("PricePerKm")
                        .HasColumnType("real")
                        .HasColumnName("price_per_km");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("status");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real")
                        .HasColumnName("total_price");

                    b.HasKey("QuoteId");

                    b.HasIndex("CarrierId");

                    b.HasIndex("ParcelId");

                    b.ToTable("quotes");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.TestUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("test_users");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.CarrierRating", b =>
                {
                    b.HasOne("MarteliveryAPI.Models.Customer", "Customer")
                        .WithMany("CarrierRatings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarteliveryAPI.Models.Delivery", "Delivery")
                        .WithMany("CarrierRatings")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Delivery");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Delivery", b =>
                {
                    b.HasOne("MarteliveryAPI.Models.Quote", "Quote")
                        .WithMany("Deliveries")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Parcel", b =>
                {
                    b.HasOne("MarteliveryAPI.Models.Customer", "Customer")
                        .WithMany("Parcels")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Payment", b =>
                {
                    b.HasOne("MarteliveryAPI.Models.Quote", "Quote")
                        .WithMany("Payments")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Quote", b =>
                {
                    b.HasOne("MarteliveryAPI.Models.Carrier", "Carrier")
                        .WithMany("Quotes")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarteliveryAPI.Models.Parcel", "Parcel")
                        .WithMany("Quotes")
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("Parcel");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Carrier", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Customer", b =>
                {
                    b.Navigation("CarrierRatings");

                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Delivery", b =>
                {
                    b.Navigation("CarrierRatings");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Parcel", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("MarteliveryAPI.Models.Quote", b =>
                {
                    b.Navigation("Deliveries");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
