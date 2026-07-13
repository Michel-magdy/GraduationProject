using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<CarRental> CarRentals { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<HotelBooking> HotelBookings { get; set; }
    public DbSet<RestaurantBooking> RestaurantBookings { get; set; }
    public DbSet<CarRentalBooking> CarRentalBookings { get; set; }
    public DbSet<TourBooking> TourBookings { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<HotelReview> HotelReviews { get; set; }
    public DbSet<RestaurantReview> RestaurantReviews { get; set; }
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<CarRentalReview> CarRentalReviews { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var foreignKey in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<Room>()
    .Property(r => r.Price)
    .HasPrecision(18, 2);

        modelBuilder.Entity<Tour>()
            .Property(t => t.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CarRental>()
            .Property(c => c.PricePerDay)
            .HasPrecision(18, 2);

        modelBuilder.Entity<HotelBooking>()
            .Property(h => h.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CarRentalBooking>()
            .Property(c => c.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<TourBooking>()
            .Property(t => t.TotalPrice)
            .HasPrecision(18, 2);
    }
}