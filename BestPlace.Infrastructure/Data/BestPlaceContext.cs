using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Infrastructure.Data;

public class BestPlaceContext : IdentityDbContext<IdentityUser>
{
    public BestPlaceContext(DbContextOptions<BestPlaceContext> options)
        : base(options)
    {
    }

}
