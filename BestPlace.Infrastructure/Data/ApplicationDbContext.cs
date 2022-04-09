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


        builder.Entity<ItemImages>()
            .HasOne(x => x.Item)
            .WithMany(h => h.Images)
            //.HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

       

        base.OnModelCreating(builder);
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Call> Calls { get; set; }

    public DbSet<Questionnaire> Questionnaires { get; set; }

    public DbSet<SubmitQuestionnaire> SubmitQuestionnaires { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<ItemImages> ItemsImages { get; set; }


}
