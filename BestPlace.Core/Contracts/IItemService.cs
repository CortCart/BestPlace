
using BestPlace.Core.Models.Item;

namespace BestPlace.Core.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemListViewModel>> All();

    Task<ItemDetailsViewModel> GetItemDetails(Guid id);

    Task Add(ItemAddViewModel model, string id);

    Task Edit(ItemAddViewModel model);

    Task DeleteItem(Guid id);
}