using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestPlace.Infrastructure.Data;

public class Category
{

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public Guid ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public Image Image { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();
}