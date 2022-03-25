using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestPlace.Infrastructure.Data;

public class ItemImages
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public byte[] Source { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
}