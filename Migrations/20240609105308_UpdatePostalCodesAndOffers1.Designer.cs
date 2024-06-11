﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShippingCalculator.Models;

#nullable disable

namespace ShippingCalculator.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240609105308_UpdatePostalCodesAndOffers1")]
    partial class UpdatePostalCodesAndOffers1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ShippingCalculator.Models.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("MaxCubic")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MaxHeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MaxLength")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MaxWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MaxWidth")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Carriers");
                });

            modelBuilder.Entity("ShippingCalculator.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("CarrierId")
                        .HasColumnType("int");

                    b.Property<decimal>("CubicMeterCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ExtraCostPerKg")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MaximumWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MinimumShippingCost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("MinimumWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("OfferType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("ShippingCalculator.Models.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarrierId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<string>("Nomos")
                        .HasColumnType("longtext");

                    b.Property<string>("Postal")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("ShippingCalculator.Models.PostalCodeOffer", b =>
                {
                    b.Property<int>("PostalCodeId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("PostalCodeId", "OfferId");

                    b.HasIndex("OfferId");

                    b.ToTable("PostalCodeOffers");
                });

            modelBuilder.Entity("ShippingCalculator.Models.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("VolumetricWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Width")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("ShippingCalculator.Models.Offer", b =>
                {
                    b.HasOne("ShippingCalculator.Models.Carrier", "Carrier")
                        .WithMany()
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrier");
                });

            modelBuilder.Entity("ShippingCalculator.Models.PostalCode", b =>
                {
                    b.HasOne("ShippingCalculator.Models.Carrier", null)
                        .WithMany("SupportedPostalCodes")
                        .HasForeignKey("CarrierId");
                });

            modelBuilder.Entity("ShippingCalculator.Models.PostalCodeOffer", b =>
                {
                    b.HasOne("ShippingCalculator.Models.Offer", "Offer")
                        .WithMany("PostalCodes")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShippingCalculator.Models.PostalCode", "PostalCode")
                        .WithMany("Offers")
                        .HasForeignKey("PostalCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("ShippingCalculator.Models.Carrier", b =>
                {
                    b.Navigation("SupportedPostalCodes");
                });

            modelBuilder.Entity("ShippingCalculator.Models.Offer", b =>
                {
                    b.Navigation("PostalCodes");
                });

            modelBuilder.Entity("ShippingCalculator.Models.PostalCode", b =>
                {
                    b.Navigation("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
