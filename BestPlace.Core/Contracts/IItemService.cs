
using BestPlace.Core.Models.Item;

namespace BestPlace.Core.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemListViewModel>> All();

    Task<ItemDetailsViewModel> GetItemDetails(Guid id);

    Task Add(ItemAddViewModel model);

    Task Edit(ItemAddViewModel model);

    Task<bool> DeleteItem(Guid id);
}