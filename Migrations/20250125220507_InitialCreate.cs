using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ipaddressmanagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IPAddress = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Hostname = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    EquipmentType = table.Column<string>(type: "TEXT", nullable: false),
                    Criticality = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    MACAddress = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Building = table.Column<string>(type: "TEXT", nullable: false),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    Room = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeType = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ChangedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_ChangeLogs_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceID", "Building", "City", "CreatedAt", "Criticality", "Department", "EquipmentType", "Floor", "Hostname", "IPAddress", "MACAddress", "PostalCode", "Room", "Status", "Street", "UpdatedAt" },
                values: new object[] { 1, "Eurostation", "Brussels", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "IT", "Dell PowerEdge R750", 5, "server-01", "192.168.1.100", "00:1A:2B:3C:4D:5E", "1000", "SR501", "Active", "Rue de la Loi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Username" },
                values: new object[] { 1, "$2a$11$p7WzGeSVIdVRSSpwvxxw7OC3C3K9j2WYCx1DAA0JIJAwnLn4aDwSe", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_DeviceID",
                table: "ChangeLogs",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IPAddress",
                table: "Devices",
                column: "IPAddress",
                unique: true); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
