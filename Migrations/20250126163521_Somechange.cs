using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ipaddressmanagement.Migrations
{
    /// <inheritdoc />
    public partial class Somechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "DeviceID",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "DeviceID",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
