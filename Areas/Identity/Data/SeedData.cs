using Microsoft.AspNetCore.Identity;
using ZalakProject.Data;
using ZalakProject.Models;

namespace ZalakProject.Areas.Identity.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Create roles
            string[] roleNames = { "Admin", "Seller", "Buyer" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user
            if (await userManager.FindByEmailAsync("admin@ecommerce.com") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@ecommerce.com",
                    Email = "admin@ecommerce.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

          

            // Seed categories
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and gadgets" },
                    new Category { Name = "Clothing", Description = "Apparel and fashion items" },
                    new Category { Name = "Books", Description = "Books and educational materials" },
                    new Category { Name = "Home & Garden", Description = "Home improvement and garden items" },
                    new Category { Name = "Sports", Description = "Sports and outdoor equipment" }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

           
        }
    }
}
