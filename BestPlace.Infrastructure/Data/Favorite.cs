using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Infrastructure.Data;

public class Favorite
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    public ICollection<Item> Favorites { get; set; } = new List<Item>();
}