﻿// <auto-generated />
using System;
using IntusWindows.Sales.Order.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntusWindows.Sales.Order.Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230326125409_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Dimension", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("Dimensions");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Element", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WindowId")
                        .HasColumnType("uuid");

                    b.Property<string>("dimensionId")
                        .HasColumnType("text");

                    b.Property<string>("elementName")
                        .HasColumnType("text");

                    b.Property<int>("elementType")
                        .HasColumnType("integer")
                        .HasColumnName("elementType");

                    b.HasKey("Id");

                    b.HasIndex("WindowId");

                    b.HasIndex("dimensionId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Window", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("QuantityOfWindows")
                        .HasMaxLength(4)
                        .HasColumnType("integer")
                        .HasColumnName("quantityOfWindows");

                    b.Property<int>("TotalSubElements")
                        .HasMaxLength(4)
                        .HasColumnType("integer")
                        .HasColumnName("totalSubElements");

                    b.Property<string>("windowName")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Windows");
                });

            modelBuilder.Entity("OrderWindow", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WindowsId")
                        .HasColumnType("uuid");

                    b.HasKey("OrderId", "WindowsId");

                    b.HasIndex("WindowsId");

                    b.ToTable("OrderWindow");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Dimension", b =>
                {
                    b.OwnsOne("IntusWindows.Sales.Order.Domain.ValueObjects.Height", "Height", b1 =>
                        {
                            b1.Property<string>("DimensionId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Value")
                                .HasMaxLength(4)
                                .HasColumnType("numeric")
                                .HasColumnName("height");

                            b1.HasKey("DimensionId");

                            b1.ToTable("Dimensions");

                            b1.WithOwner()
                                .HasForeignKey("DimensionId");
                        });

                    b.OwnsOne("IntusWindows.Sales.Order.Domain.ValueObjects.Width", "Width", b1 =>
                        {
                            b1.Property<string>("DimensionId")
                                .HasColumnType("text");

                            b1.Property<decimal>("Value")
                                .HasMaxLength(4)
                                .HasColumnType("numeric")
                                .HasColumnName("width");

                            b1.HasKey("DimensionId");

                            b1.ToTable("Dimensions");

                            b1.WithOwner()
                                .HasForeignKey("DimensionId");
                        });

                    b.Navigation("Height");

                    b.Navigation("Width");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Element", b =>
                {
                    b.HasOne("IntusWindows.Sales.Order.Domain.Entities.Window", null)
                        .WithMany("SubElements")
                        .HasForeignKey("WindowId");

                    b.HasOne("IntusWindows.Sales.Order.Domain.Entities.Dimension", "dimension")
                        .WithMany()
                        .HasForeignKey("dimensionId");

                    b.Navigation("dimension");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Order", b =>
                {
                    b.OwnsOne("IntusWindows.Sales.Order.Domain.ValueObjects.OrderName", "OrderName", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("orderName");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("OrderName");
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.State", b =>
                {
                    b.OwnsOne("IntusWindows.Sales.Order.Domain.ValueObjects.StateName", "Name", b1 =>
                        {
                            b1.Property<Guid>("StateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("name");

                            b1.HasKey("StateId");

                            b1.ToTable("States");

                            b1.WithOwner()
                                .HasForeignKey("StateId");
                        });

                    b.Navigation("Name");
                });

            modelBuilder.Entity("OrderWindow", b =>
                {
                    b.HasOne("IntusWindows.Sales.Order.Domain.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntusWindows.Sales.Order.Domain.Entities.Window", null)
                        .WithMany()
                        .HasForeignKey("WindowsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntusWindows.Sales.Order.Domain.Entities.Window", b =>
                {
                    b.Navigation("SubElements");
                });
#pragma warning restore 612, 618
        }
    }
}
