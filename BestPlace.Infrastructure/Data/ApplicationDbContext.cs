using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;
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
        //this.Database.EnsureCreated();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Item>()
            .HasOne(x => x.Owner)
            .WithMany(x => x.MyItems);


        builder.Entity<Deal>().HasKey(x => x.Id);

        builder.Entity<Deal>().HasOne(x => x.BuyerUser)
            .WithMany(d => d.DealsAsBuyer)
            .HasForeignKey(x => x.BuyerUserId)
            .OnDelete(DeleteBehavior.Restrict)
            ;

        builder.Entity<Deal>().HasOne(x=>x.Delivery)
            .WithMany()
            .HasForeignKey(x=>x.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict)
           ;

        builder.Entity<Deal>().HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Restrict)
            ;

        builder.Entity<Deal>().HasOne(x=>x.ExOwner)
            .WithMany(c=>c.DealsAsOwner)
            .HasForeignKey(x=>x.ExOwnerId)
            .OnDelete(DeleteBehavior.Restrict)
            ;


        base.OnModelCreating(builder);
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Deal> Deals { get; set; }

    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Questionnaire> Questionnaires { get; set; }

    public DbSet<SubmitQuestionnaire> SubmitQuestionnaires { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<ItemImages> ItemImages { get; set; }
}
