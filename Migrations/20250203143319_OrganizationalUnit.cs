using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ipaddressmanagement.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationalUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ChangedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    StreetName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    StreetNumber = table.Column<string>(type: "TEXT", nullable: false),
                    LowestFloor = table.Column<int>(type: "INTEGER", nullable: false),
                    HighestFloor = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    OrganizationalUnit = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IPAddress = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Hostname = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EquipmentType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Criticality = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    MACAddress = table.Column<string>(type: "TEXT", nullable: false),
                    BuildingId = table.Column<int>(type: "INTEGER", nullable: false),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    Room = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Devices_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Buildings",
                columns: new[] { "Id", "CityName", "CreatedAt", "CreatedBy", "HighestFloor", "LowestFloor", "Name", "NumberOfRooms", "OrganizationalUnit", "ShortName", "StreetName", "StreetNumber", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, "Brussels", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", 20, 5, "Eurostation", 50, "HQ", "ES", "Rue de la Loi", "12A", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "PasswordHash", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", "$2a$11$p7WzGeSVIdVRSSpwvxxw7OC3C3K9j2WYCx1DAA0JIJAwnLn4aDwSe", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", "admin" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceID", "BuildingId", "CreatedAt", "CreatedBy", "Criticality", "Department", "EquipmentType", "Floor", "Hostname", "IPAddress", "MACAddress", "Room", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", "High", "IT", "Dell PowerEdge R750", 5, "server-01", "192.168.1.100", "00:1A:2B:3C:4D:5E", "SR501", "Active", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData" });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Name_StreetName_StreetNumber",
                table: "Buildings",
                columns: new[] { "Name", "StreetName", "StreetNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLogs_DeviceID",
                table: "ChangeLogs",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BuildingId",
                table: "Devices",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IPAddress",
                table: "Devices",
                column: "IPAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ChangeLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
