using GraduationProject.Interfaces;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IBusiness, BusinessService>();
builder.Services.AddScoped<ICarRentalBooking, CarRentalBookingService>();
builder.Services.AddScoped<ICarRental, CarRentalService>();
builder.Services.AddScoped<IHotelBooking, HotelBookingService>();
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<IRestaurantBooking, RestaurantBookingService>();
builder.Services.AddScoped<IRestaurant, RestaurantService>();
builder.Services.AddScoped<IReview, ReviewService>();
builder.Services.AddScoped<IRoom, RoomService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<ITable, TableService>();
builder.Services.AddScoped<ITourBooking, TourBookingService>();
builder.Services.AddScoped<ITour, TourService>();

builder.Services.AddDbContext<Context>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("CS")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
