using System.ComponentModel;

namespace BestPlace.Core.Models.User;

public class UserDealAsOwnerViewModel
{
    public string BuyerId { get; set; }

    [DisplayName("Buyer name")]
    public string BuyerName { get; set; }

    public Guid ItemId { get; set; }

    [DisplayName("Item name")]
    public string ItemName { get; set; }
}