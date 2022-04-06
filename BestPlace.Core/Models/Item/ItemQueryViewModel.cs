namespace BestPlace.Core.Models.Item;

public class ItemQueryViewModel
{
    public Guid CategoryId { get; set; }

    public string Query { get; set; }

    public IEnumerable<ItemPublicListViewModel> Items { get; set; } = new List<ItemPublicListViewModel>();
}