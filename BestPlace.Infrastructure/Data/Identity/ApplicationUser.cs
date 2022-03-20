using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BestPlace.Infrastructure.Data.Identity;

public class ApplicationUser:IdentityUser
{
    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }
}