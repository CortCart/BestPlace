using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BestPlace.Infrastructure.Data.Identity;

public class ApplicationUser:IdentityUser
{
    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(250)]
    public string Addres { get; set; }

    [Required]
    [RegularExpression("[+]{1}359 [0-9]{3} [0-9]{4}")]
    public string Phone { get; set; }

    public ICollection<Item> FavouriteItems { get; set; } = new List<Item>();

    public ICollection<Item> MyItems { get; set; } = new List<Item>();
}