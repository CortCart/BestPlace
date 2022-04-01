using System.ComponentModel;

namespace BestPlace.Core.Models.User;

public class UserDealAsBuyerViewModel
{
    public string ExOwnerId { get; set; }

    [DisplayName("ExOwner name")]
    public string ExOwnerName { get; set; }

    public Guid ItemId { get; set; }

    [DisplayName("Item name")]
    public string ItemName { get; set; }
}