using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_WebApi7.Migrations
{
    /// <inheritdoc />
    public partial class DataTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Villas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa...", new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4153), new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4137), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de la villa...", new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4159), new DateTime(2023, 12, 31, 22, 16, 27, 610, DateTimeKind.Local).AddTicks(4158), "", 40, "Premium Vista a la Piscina", 4, 285.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Villas");
        }
    }
}
