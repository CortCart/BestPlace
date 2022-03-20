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

    public int Likes { get; set; }

    [Required]
    public string OwenerId { get; set; }

    [ForeignKey(nameof(OwenerId))]
    public ApplicationUser Owner { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();
}