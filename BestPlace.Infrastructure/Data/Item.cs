using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Infrastructure.Data;

public class Item
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Label { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range((typeof(decimal)), "0" , "79228162514264337593543950335")]
    [Required]
    public decimal Price { get; set; }

    public int Likes { get; set; } = 0;

    [Required]
    public string OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public ApplicationUser Owner { get; set; }

    public bool IsBought { get; set; } = false;


    [Required]
    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    public ICollection<ItemImages> Images { get; set; } = new List<ItemImages>();
}