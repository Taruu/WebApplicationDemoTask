﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationGuide.Models;

namespace WebApplicationGuide.Migrations
{
    [DbContext(typeof(ElectricityСountContext))]
    [Migration("20220402121722_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1");

            modelBuilder.Entity("WebApplicationGuide.Models.ElectricityCount", b =>
                {
                    b.Property<long>("ElectricityCountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("ElectricityCountId");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.ToTable("ElectricityCount");
                });

            modelBuilder.Entity("WebApplicationGuide.Models.ElectricityValue", b =>
                {
                    b.Property<long>("ElectricityValuesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("ActiveOutput")
                        .HasColumnType("REAL");

                    b.Property<float>("ActiveReceive")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("TEXT");

                    b.Property<long>("ElectricityCountForeignKey")
                        .HasColumnType("INTEGER");

                    b.Property<float>("ReactiveOutput")
                        .HasColumnType("REAL");

                    b.Property<float>("ReactiveReceive")
                        .HasColumnType("REAL");

                    b.HasKey("ElectricityValuesId");

                    b.HasIndex("ElectricityCountForeignKey");

                    b.ToTable("ElectricityValues");
                });

            modelBuilder.Entity("WebApplicationGuide.Models.ElectricityValue", b =>
                {
                    b.HasOne("WebApplicationGuide.Models.ElectricityCount", "ElectricityCount")
                        .WithMany()
                        .HasForeignKey("ElectricityCountForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}