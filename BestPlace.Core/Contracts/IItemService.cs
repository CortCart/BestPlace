using BestPlace.Core.Models.Item;

namespace BestPlace.Core.Contracts;

public interface IItemService
{
    Task<ItemQueryViewModel> AllPublic(Guid categoryId, string query);

    Task<IEnumerable<ItemListViewModel>> All();


    Task IsMine(Guid id, string userId);

    Task<ItemEditViewModel> GetItemForEdit(Guid id, string userId);

    Task Edit(ItemEditViewModel model, string userId);

    Task<ItemDetailsViewModel> GetItemDetails(Guid id);

    Task<ItemDetailsViewModel> GetItemDetailsAsAdmin(Guid id);

    Task<bool> AddItem(ItemAddViewModel model, string userId);


    Task DeleteItem(Guid id, string userId);

    Task DeleteItemAsAdmin(Guid id);

}