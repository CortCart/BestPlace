using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>(x =>
        {
            x.HasMany(d => d.FavouriteItems).WithOne(e => e.Owner).HasForeignKey(e=>e.OwenerId).OnDelete(DeleteBehavior.Restrict);
        });
     
        base.OnModelCreating(builder);
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Deal> Deals { get; set; }

    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Item> Items { get; set; }
}
