﻿// <auto-generated />
using System;
using CustomersCanvasSample.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CustomersCanvasSampleMVC.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    [Migration("20210201133149_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CustomersCanvasSample.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<int?>("TemplateId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = "7013145727",
                            ImageUrl = "https://picsum.photos/id/1025/600/800",
                            Name = "Test Product 1",
                            Price = 14.99f
                        },
                        new
                        {
                            Id = "4245545145",
                            ImageUrl = "https://picsum.photos/id/1026/600/800",
                            Name = "Test Product 2",
                            Price = 9.99f
                        },
                        new
                        {
                            Id = "1253932525",
                            ImageUrl = "https://picsum.photos/id/1027/600/800",
                            Name = "Test Product 3",
                            Price = 19.99f
                        },
                        new
                        {
                            Id = "9559586705",
                            ImageUrl = "https://picsum.photos/id/1028/600/800",
                            Name = "Test Product 4",
                            Price = 4.99f
                        },
                        new
                        {
                            Id = "1540922619",
                            ImageUrl = "https://picsum.photos/id/1029/600/800",
                            Name = "Test Product 5",
                            Price = 1.99f
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
