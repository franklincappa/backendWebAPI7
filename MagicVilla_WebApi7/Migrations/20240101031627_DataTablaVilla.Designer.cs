﻿// <auto-generated />
using System;
using MagicVilla_WebApi7.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_WebApi7.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240101031627_DataTablaVilla")]
    partial class DataTablaVilla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_WebApi7.Model.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de la villa...",
                            FechaActualizacion = new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4153),
                            FechaCreacion = new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4137),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Villa Real",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle de la villa...",
                            FechaActualizacion = new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4159),
                            FechaCreacion = new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4158),
                            ImagenUrl = "",
                            MetrosCuadrados = 40,
                            Nombre = "Premium Vista a la Piscina",
                            Ocupantes = 4,
                            Tarifa = 285.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
