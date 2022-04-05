using System.Linq;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models;
using BestPlace.Core.Models.Deal;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class DealService:IDealService
{
    private readonly IApplicatioDbRepository repository;

    public DealService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<DealListViewModel>> All()
    {
        var deals = await this.repository.All<Deal>()
            .Select(x => new DealListViewModel()
            {
                DealId = x.Id,
                DealFor = x.Item.Label,
            }).ToListAsync();

        return deals;
    }

    public async Task<DealDetailsViewModel> GetDealDetails(Guid id)
    {
        var model = await this.repository.GetByIdAsync<Deal>(id);
        if (model == null) throw new ArgumentException("Unknown deal");

        return new DealDetailsViewModel()
        {
            Id = model.Id,
            BuyerId = model.BuyerUserId,
            BuyerName = $"{model.BuyerUser.FirstName} {model.BuyerUser.LastName}",
            ExOnwerId = model.ExOwnerId,
            ExOnwerName = $"{model.ExOwner.FirstName} {model.ExOwner.LastName}",
            ItemId = model.ItemId,
            ItemName = model.Item.Label,
            DeliveryAddress = model.Delivery.Addres,
            DeliveryDescription = model.Delivery.Description
        };
    }
}