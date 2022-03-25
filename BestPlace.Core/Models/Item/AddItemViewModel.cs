using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Core.Models.Item;

public class AddItemViewModel
{

    [Required]
    [MaxLength(50)]
    public string Label { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range((typeof(decimal)), "0", "79228162514264337593543950335")]
    [Required]
    public decimal Price { get; set; }

    public string Category { get; set; }

    public ICollection<byte[]> Images { get; set; } = new List<byte[]>();
}