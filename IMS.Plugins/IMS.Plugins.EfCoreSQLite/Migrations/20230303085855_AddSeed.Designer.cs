﻿// <auto-generated />
using System;
using IMS.Plugins.EfCoreSQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IMS.Plugins.EfCoreSQLite.Migrations
{
    [DbContext(typeof(ImsDbContext))]
    [Migration("20230303085855_AddSeed")]
    partial class AddSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("IMS.CoreBusiness.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Inventories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bike Seat",
                            Price = 2.0,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bike Body",
                            Price = 15.0,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bike Wheels",
                            Price = 8.0,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 4,
                            Name = "Bike Pedels",
                            Price = 1.0,
                            Quantity = 20
                        });
                });

            modelBuilder.Entity("IMS.CoreBusiness.InventoryTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DoneBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("InventoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PoNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductionNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityAfter")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityBefore")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.ToTable("InventoryTransactions");
                });

            modelBuilder.Entity("IMS.CoreBusiness.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Bike",
                            Price = 150.0,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 2,
                            Name = "Car",
                            Price = 25000.0,
                            Quantity = 5
                        });
                });

            modelBuilder.Entity("IMS.CoreBusiness.ProductInventory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InventoryQuality")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "InventoryId");

                    b.HasIndex("InventoryId");

                    b.ToTable("ProductInventories");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            InventoryId = 1,
                            InventoryQuality = 1
                        },
                        new
                        {
                            ProductId = 1,
                            InventoryId = 2,
                            InventoryQuality = 1
                        },
                        new
                        {
                            ProductId = 1,
                            InventoryId = 3,
                            InventoryQuality = 2
                        },
                        new
                        {
                            ProductId = 1,
                            InventoryId = 4,
                            InventoryQuality = 2
                        });
                });

            modelBuilder.Entity("IMS.CoreBusiness.ProductTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActivityType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DoneBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductionNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityAfter")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityBefore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SoNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.Property<double?>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductTransactions");
                });

            modelBuilder.Entity("IMS.CoreBusiness.InventoryTransaction", b =>
                {
                    b.HasOne("IMS.CoreBusiness.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("IMS.CoreBusiness.ProductInventory", b =>
                {
                    b.HasOne("IMS.CoreBusiness.Inventory", "Inventory")
                        .WithMany("ProductInventories")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMS.CoreBusiness.Product", "Product")
                        .WithMany("ProductInventories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IMS.CoreBusiness.ProductTransaction", b =>
                {
                    b.HasOne("IMS.CoreBusiness.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("IMS.CoreBusiness.Inventory", b =>
                {
                    b.Navigation("ProductInventories");
                });

            modelBuilder.Entity("IMS.CoreBusiness.Product", b =>
                {
                    b.Navigation("ProductInventories");
                });
#pragma warning restore 612, 618
        }
    }
}
