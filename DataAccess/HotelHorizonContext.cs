using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class HotelHorizonContext : DbContext
    {
        private readonly string _connectionString;

        public HotelHorizonContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public HotelHorizonContext()
        {
            _connectionString = "Data Source=DESKTOP-QHG3HPJ\\SQLEXPRESS;Initial Catalog=horizon_hotel;Integrated Security=True;Trust Server Certificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<UserUseCase>().HasKey(x => new
            {
                x.UserId,
                x.UseCaseId
            });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();
            
            foreach (EntityEntry entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity entity)
                    {
                        entity.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Domain.Type> Types { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBed> RoomBeds { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OccupiedRoom> OccupiedRooms { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
    }
}
