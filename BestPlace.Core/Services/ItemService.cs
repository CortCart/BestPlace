using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Item;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class ItemService : IItemService
{
    private readonly IApplicatioDbRepository repository;

    public ItemService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task Add(AddItemViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task Edit(AddItemViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task Remove()
    {
        throw new NotImplementedException();
    }
}