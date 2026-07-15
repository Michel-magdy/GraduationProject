using GraduationProject.Models;

namespace GraduationProject.Data;

public static class DbSeeder
{
    public static void Seed(Context context)
    {
        // 1. Ensure Roles exist
        var roles = new[] { "Admin", "Owner", "Customer" };
        foreach (var roleName in roles)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                context.Roles.Add(new Role { Name = roleName });
            }
        }
        context.SaveChanges();

        // 2. Ensure an Admin user exists
        var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
        if (adminRole != null && !context.Users.Any(u => u.Email == "admin@nexustravel.com"))
        {
            context.Users.Add(new User
            {
                FullName = "System Admin",
                Email = "admin@nexustravel.com",
                PasswordHash = "admin123", // Simple plain-text password for this project
                Phone = "1234567890",
                RoleId = adminRole.Id,
                CreatedAt = DateTime.UtcNow
            });
            context.SaveChanges();
        }
    }
}
