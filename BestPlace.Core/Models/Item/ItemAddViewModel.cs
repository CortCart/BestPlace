using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;

namespace BestPlace.Core.Models.Item;

public class ItemAddViewModel
{

    [Required]
    [MaxLength(50)]
    public string Label { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range((typeof(decimal)), "0", "79228162514264337593543950335")]
    [Required]
    public decimal Price { get; set; }

    [Required]
    public string CategoryId { get; set; }

    [MinLength(1, ErrorMessage = "The field Images must have at least 1 image")]
    public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
}