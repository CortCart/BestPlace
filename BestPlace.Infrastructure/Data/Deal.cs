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

    public string ExOwnerId { get; set; }

    [ForeignKey(nameof(ExOwnerId))]
    public ApplicationUser ExOwner { get; set; }

    public string BuyerUserId { get; set; }

    [ForeignKey(nameof(BuyerUserId))]
    public ApplicationUser BuyerUser { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
}