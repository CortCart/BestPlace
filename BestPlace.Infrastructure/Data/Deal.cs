using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Infrastructure.Data;

public class Deal
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid DeliveryId { get; set; }

    [ForeignKey(nameof(DeliveryId))]
    public Delivery Delivery { get; set; }

    [Required]
    public string OwenerId { get; set; }

    [ForeignKey(nameof(OwenerId))]
    public ApplicationUser Owner { get; set; }

    [Required]
    public string BuyerId { get; set; }

    [ForeignKey(nameof(BuyerId))]
    public ApplicationUser Buyer { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
}