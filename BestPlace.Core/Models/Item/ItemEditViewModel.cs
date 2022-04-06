using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace BestPlace.Core.Models.Item;

public class ItemEditViewModel
{
    public Guid Id { get; set; }

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

    public ICollection<ItemImageDetailsViewModel> ViewImage { get; set; } = new List<ItemImageDetailsViewModel>();



    public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
}