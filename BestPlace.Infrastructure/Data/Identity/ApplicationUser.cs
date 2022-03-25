using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace BestPlace.Infrastructure.Data.Identity;

public class ApplicationUser:IdentityUser
{

    [StringLength(50)]
    public string FirstName { get; set; }

    [AllowNull]
    public byte[] Image { get; set; }


    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(250)]
    public string Address { get; set; }

    [Required]
    [RegularExpression("[+]{1}359 [0-9]{3} [0-9]{4}")]
    public string Phone { get; set; }

    public ICollection<Item> MyItems { get; set; } = new List<Item>();

    public ICollection<Deal> DealsAsOwner { get; set; } = new List<Deal>();

    public ICollection<Deal> DealsAsBuyer{ get; set; } = new List<Deal>();

}