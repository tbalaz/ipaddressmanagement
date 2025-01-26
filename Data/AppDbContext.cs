using Microsoft.EntityFrameworkCore;
using IPAddressManagement.Models;

namespace IPAddressManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
            
            // Configure relationships
            modelBuilder.Entity<ChangeLog>()
                .HasOne(cl => cl.Device)
                .WithMany(d => d.ChangeLogs)
                .HasForeignKey(cl => cl.DeviceID);

            // Seed initial data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "$2a$11$p7WzGeSVIdVRSSpwvxxw7OC3C3K9j2WYCx1DAA0JIJAwnLn4aDwSe"
                }
            );

            // Seed sample device
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
                    City = "Brussels",
                    PostalCode = "1000",
                    Street = "Rue de la Loi",
                    Building = "Eurostation",
                    Floor = 5,
                    Room = "SR501",
                    CreatedAt = new DateTime(2023, 1, 1),
                    UpdatedAt = new DateTime(2023, 1, 1) 
                }
            );
        }
    }
}