namespace BestPlace.Core.Models.Item;

public class ItemPublicListViewModel
{
    public Guid Id { get; set; }

    public string Label { get; set; }

    public decimal Price { get; set; }

    public ItemImageDetailsViewModel Image { get; set; } 

}