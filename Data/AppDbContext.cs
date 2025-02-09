using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Models;

namespace IPAddressManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Device entity
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(d => d.DeviceID);
                entity.Property(d => d.IPAddress).IsRequired().HasMaxLength(45);
                entity.HasIndex(d => d.IPAddress).IsUnique();
                entity.Property(d => d.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(d => d.Criticality).HasConversion<string>().HasMaxLength(10);
            });

            // Configure ChangeLog
            modelBuilder.Entity<ChangeLog>()
                .HasKey(cl => cl.LogID);
            modelBuilder.Entity<ChangeLog>()
                .HasOne(cl => cl.Device)
                .WithMany(d => d.ChangeLogs)
                .HasForeignKey(cl => cl.DeviceID);

            // Configure AuditLog (optional)
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLogs");
                entity.Property(a => a.EntityType).HasMaxLength(100);
            });

            // One-to-many: Building -> Devices
            modelBuilder.Entity<Building>()
                .HasMany(b => b.Devices)
                .WithOne(d => d.Building)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Unique index on Name + StreetName + StreetNumber
            modelBuilder.Entity<Building>()
                .HasIndex(b => new { b.Name, b.StreetName, b.StreetNumber })
                .IsUnique();

            // ----------------- SEED DATA -----------------

            // 1) Seed a user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "$2a$11$p7WzGeSVIdVRSSpwvxxw7OC3C3K9j2WYCx1DAA0JIJAwnLn4aDwSe",
                    CreatedAt = new DateTime(2023, 1, 1),
                    UpdatedAt = new DateTime(2023, 1, 1),
                    CreatedBy = "SeedData",
                    UpdatedBy = "SeedData"
                }
            );

            // 2) Seed a Building with Id = 1
            modelBuilder.Entity<Building>().HasData(
                new Building
                {
                    Id = 1,
                    Name = "Eurostation",
                    CityName = "Brussels",
                    OrganizationalUnit = "HQ",
                    StreetName = "Rue de la Loi",
                    StreetNumber = "12A", // for example
                    LowestFloor = 5,
                    HighestFloor = 20,
                    NumberOfRooms = 50,
                    ShortName = "ES",
                    CreatedAt = new DateTime(2023, 1, 1),
                    CreatedBy = "SeedData",
                    UpdatedAt = new DateTime(2023, 1, 1),
                    UpdatedBy = "SeedData"
                }
            );

            // 3) Seed a Device that references BuildingId = 1
            modelBuilder.Entity<Device>().HasData(
                new Device
                {
                    DeviceID = 1,
                    IPAddress = "192.168.1.100",
                    Status = DeviceStatus.Active,
                    Hostname = "server-01",
                    Department = "IT",
                    EquipmentType = "Dell PowerEdge R750",
                    Criticality = CriticalityLevel.High,
                    MACAddress = "00:1A:2B:3C:4D:5E",
                    BuildingId = 1,
                    Floor = 5,
                    Room = "SR501",
                    CreatedAt = new DateTime(2023, 1, 1),
                    UpdatedAt = new DateTime(2023, 1, 1),
                    CreatedBy = "SeedData",
                    UpdatedBy = "SeedData"
                }
            );
        }
    }
}