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
    }
}