using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestPlace.Core.Models.Deal;

public class DealDetailsViewModel
{
    public Guid Id { get; set; }

    public string ExOnwerId { get; set; }

    public string ExOnwerName { get; set; }

    public string BuyerId { get; set; }

    public string BuyerName { get; set; }

    public Guid ItemId { get; set; }

    public string ItemName { get; set; }

    public string DeliveryAddress { get; set; }

    public string DeliveryDescription { get; set; }
}