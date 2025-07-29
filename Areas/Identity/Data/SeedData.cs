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

            // Create sample seller
            if (await userManager.FindByEmailAsync("seller@ecommerce.com") == null)
            {
                var sellerUser = new ApplicationUser
                {
                    UserName = "seller@ecommerce.com",
                    Email = "seller@ecommerce.com",
                    FirstName = "John",
                    LastName = "Seller",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(sellerUser, "Seller123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sellerUser, "Seller");
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

            // Seed sample products
            if (!context.Products.Any())
            {
                var seller = await userManager.FindByEmailAsync("seller@ecommerce.com");
                if (seller != null)
                {
                    var products = new[]
                    {
                        new Product
                        {
                            Name = "Smartphone XY",
                            Description = "Latest smartphone with advanced features",
                            Price = 599.99,
                            CategoryId = 1,
                            StockQuantity = 50,
                            SellerId = seller.Id
                        },
                        new Product
                        {
                            Name = "Cotton T-Shirt",
                            Description = "Comfortable cotton t-shirt in various colors",
                            Price = 29.99,
                            CategoryId = 2,
                            StockQuantity = 100,
                            SellerId = seller.Id
                        },
                        new Product
                        {
                            Name = "Programming Guide",
                            Description = "Comprehensive guide to modern programming",
                            Price = 45.99,
                            CategoryId = 3,
                            StockQuantity = 25,
                            SellerId = seller.Id
                        }
                    };

                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
