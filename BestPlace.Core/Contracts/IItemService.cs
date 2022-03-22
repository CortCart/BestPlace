
using BestPlace.Core.Models.Item;

namespace BestPlace.Core.Contracts;

public interface IItemService
{
    Task Add(AddItemViewModel model);

    Task Edit(AddItemViewModel model);

    Task Remove();
}