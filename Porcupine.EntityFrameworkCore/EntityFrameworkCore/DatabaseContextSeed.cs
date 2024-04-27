using Microsoft.EntityFrameworkCore;
using Porcupine.Core.Entities;

namespace Porcupine.EntityFrameworkCore.EntityFrameworkCore
{
    public static class DatabaseContextSeed
    {
        public static async Task SeedDatabaseAsync(DatabaseContext context)
        {
            try
            {
                // Ensure the database is created
                await context.Database.EnsureCreatedAsync();

                if (!await context.Users.AnyAsync())
                {
                    // Seed data if the table is empty
                    if (!context.Users.Any())
                    {
                        var seedData = new List<User>
                        {
                            new  User { UserName = "admin", Email = "admin@admin.com", Name = "Thapelo", Surname= "Mokole", EmailConfirmed = true }
                        };

                        await context.Users.AddRangeAsync(seedData);
                        await context.SaveChangesAsync();

                        Console.WriteLine("Data seeding completed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Database already seeded.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}