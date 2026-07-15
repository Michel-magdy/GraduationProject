using GraduationProject.Data;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IBusiness, BusinessService>();

builder.Services.AddScoped<ICarRental, CarRentalService>();
builder.Services.AddScoped<ICarRentalReview, CarRentalReviewService>();
builder.Services.AddScoped<ICarRentalBooking, CarRentalBookingService>();

builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<IHotelReview, HotelReviewService>();
builder.Services.AddScoped<IHotelBooking, HotelBookingService>();

builder.Services.AddScoped<IRestaurant, RestaurantService>();
builder.Services.AddScoped<IRestaurantReview, RestaurantReviewService>();
builder.Services.AddScoped<IRestaurantBooking, RestaurantBookingService>();

builder.Services.AddScoped<ITour, TourService>();
builder.Services.AddScoped<ITourReview, TourReviewService>();
builder.Services.AddScoped<ITourBooking, TourBookingService>();

builder.Services.AddScoped<IRoom, RoomService>();
builder.Services.AddScoped<ITable, TableService>();

// Session configuration
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<Context>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("CS")
    )
);

var app = builder.Build();

// Seed the database with default roles and admin
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    DbSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
