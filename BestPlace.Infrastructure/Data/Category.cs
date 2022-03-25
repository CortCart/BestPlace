using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestPlace.Infrastructure.Data;

public class Category
{

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public string Name { get; set; }

    public byte[] Image { get; set; }

    public ICollection<Item> Items { get; set; } = new List<Item>();
}