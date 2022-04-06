using BestPlace.Core.Models.Item;

namespace BestPlace.Core.Contracts;

public interface IItemService
{
    Task<ItemQueryViewModel> AllPublic(Guid categoryId, string query);

    Task<IEnumerable<ItemListViewModel>> All();

    Task<ItemDetailsViewModel> GetItemDetails(Guid id);

    Task IsMine(Guid id, string userId);

    Task<ItemEditViewModel> GetItemForEdit(Guid id, string userId);

    Task Edit(ItemEditViewModel model, string userId);

    Task<ItemDetailsViewModel> GetItemDetails(Guid id, string userId);

    Task<ItemDetailsViewModel> GetItemDetailsAsAdmin(Guid id);

    Task Add(ItemAddViewModel model, string userId);


    Task DeleteItem(Guid id);
}