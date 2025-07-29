using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZalakProject.Models;

namespace ZalakProject.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}
	public DbSet<Product> Products { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<ProductImage> ProductImages { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Order> Orders { get; set; }
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);


		// Configure relationships
		builder.Entity<Product>()
			.HasOne(p => p.Seller)
			.WithMany(u => u.Products)
			.HasForeignKey(p => p.SellerId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Product>()
			.HasOne(p => p.Category)
			.WithMany(c => c.Products)
			.HasForeignKey(p => p.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<ProductImage>()
			.HasOne(pi => pi.Product)
			.WithMany(p => p.ProductImages)
			.HasForeignKey(pi => pi.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Review>()
			.HasOne(r => r.Product)
			.WithMany(p => p.Reviews)
			.HasForeignKey(r => r.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<Review>()
			.HasOne(r => r.User)
			.WithMany(u => u.Reviews)
			.HasForeignKey(r => r.UserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
