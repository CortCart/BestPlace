namespace BestPlace.Core.Models.Item;

public class ItemDetailsViewModel
{
    public Guid Id { get; set; }

    public string Label { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Likes { get; set; } = 0;

    public bool IsBought { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string OwnerId { get; set; }

    public string OwnerName { get; set; }

    public string BuyerId { get; set; }

    public string BuyerName { get; set; }

    public ICollection<ItemImageDetailsViewModel> Images { get; set; } = new List<ItemImageDetailsViewModel>();

}