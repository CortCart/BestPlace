using BestPlace.Infrastructure.Data;

namespace BestPlace.Core.Models.Item;

public class ItemListViewModel
{
    public Guid Id { get; set; }

    public string Label { get; set; }

    public decimal Price { get; set; }

    public int Likes { get; set; } = 0;

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string OwnerId { get; set; }

    public string Owner { get; set; }

    public ICollection<ItemImageDetailsViewModel> Type { get; set; } = new List<ItemImageDetailsViewModel>();

}