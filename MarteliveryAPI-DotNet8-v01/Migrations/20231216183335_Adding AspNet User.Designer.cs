﻿// <auto-generated />
using System;
using MarteliveryAPI_DotNet8_v01.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarteliveryAPI_DotNet8_v01.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231216183335_Adding AspNet User")]
    partial class AddingAspNetUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Carrier", b =>
                {
                    b.Property<Guid>("CarrierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
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

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.CarrierRating", b =>
                {
                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uuid")
                        .HasColumnName("delivery_id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<int>("CarrierRate")
                        .HasColumnType("integer")
                        .HasColumnName("carrier_rate");

                    b.HasKey("DeliveryId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("carrier_ratings");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

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

                    b.HasKey("CustomerId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Delivery", b =>
                {
                    b.Property<Guid>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("delivery_id");

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("delivery_status");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delivery_time");

                    b.Property<DateTime>("PickupTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("pickup_time");

                    b.Property<Guid>("QuoteId")
                        .HasColumnType("uuid")
                        .HasColumnName("quote_id");

                    b.HasKey("DeliveryId");

                    b.HasIndex("QuoteId");

                    b.ToTable("deliveries");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Parcel", b =>
                {
                    b.Property<Guid>("ParcelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("parcel_id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
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

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
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

                    b.Property<Guid>("QuoteId")
                        .HasColumnType("uuid")
                        .HasColumnName("quote_id");

                    b.HasKey("PaymentId");

                    b.HasIndex("QuoteId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Quote", b =>
                {
                    b.Property<Guid>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("quote_id");

                    b.Property<Guid>("CarrierId")
                        .HasColumnType("uuid")
                        .HasColumnName("carrier_id");

                    b.Property<float>("DeliveryDistance")
                        .HasColumnType("real")
                        .HasColumnName("delivery_distance");

                    b.Property<Guid>("ParcelId")
                        .HasColumnType("uuid")
                        .HasColumnName("parcel_id");

                    b.Property<float>("PricePerKm")
                        .HasColumnType("real")
                        .HasColumnName("price_per_km");

                    b.Property<string>("Status")
                        .IsRequired()
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.CarrierRating", b =>
                {
                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Customer", "Customer")
                        .WithMany("CarrierRatings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Delivery", "Delivery")
                        .WithMany("CarrierRatings")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Delivery");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Delivery", b =>
                {
                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Quote", "Quote")
                        .WithMany("Deliveries")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Parcel", b =>
                {
                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Customer", "Customer")
                        .WithMany("Parcels")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Payment", b =>
                {
                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Quote", "Quote")
                        .WithMany("Payments")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Quote", b =>
                {
                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Carrier", "Carrier")
                        .WithMany("Quotes")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarteliveryAPI_DotNet8_v01.Models.Parcel", "Parcel")
                        .WithMany("Quotes")
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");

                    b.Navigation("Parcel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Carrier", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Customer", b =>
                {
                    b.Navigation("CarrierRatings");

                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Delivery", b =>
                {
                    b.Navigation("CarrierRatings");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Parcel", b =>
                {
                    b.Navigation("Quotes");
                });

            modelBuilder.Entity("MarteliveryAPI_DotNet8_v01.Models.Quote", b =>
                {
                    b.Navigation("Deliveries");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
