using System.ComponentModel;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Item;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class ItemService : IItemService
{
    private readonly IApplicatioDbRepository repository;

    public ItemService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<ItemListViewModel>> All()
    {
        var  items = await  this.repository.All<Item>()
            .Select(x=>new ItemListViewModel
            {
              Id  = x.Id,
              Label = x.Label,
              Likes = x.Likes,
              CategoryId = x.CategoryId,
              CategoryName = x.Category.Name,
              OwnerId = x.OwnerId,
              Owner = $"{x.Owner.FirstName} {x.Owner.LastName}"
            }).ToListAsync();
      return items; 
    }

    public async Task<ItemDetailsViewModel> GetItemDetails(Guid id)
    {
        var item = await this.repository.All<Item>()
            .Where(x => x.Id == id)
            .Select(x => new ItemDetailsViewModel
            {
                Id = x.Id,
                Label = x.Label,
                Description = x.Description,
                IsBought = x.IsBought,
                OwnerId = x.OwnerId,
                OwnerName = $"{x.Owner.FirstName} {x.Owner.LastName}",
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Images = x.Images.Select(y=>new ItemImageDetailsViewModel
                {
                   Id = y.Id,
                   Source = y.Source
                }).ToList()
            }).FirstOrDefaultAsync();
        item.BuyerId = await this.repository.All<Deal>().Where(x=>x.ItemId==item.Id).Select(x=>x.BuyerUserId).FirstOrDefaultAsync();
        item.BuyerName = await this.repository.All<Deal>().Where(x => x.ItemId == item.Id).Select(x => $"{x.BuyerUser.FirstName} {x.BuyerUser.LastName}").FirstOrDefaultAsync();
        return item;
    }

    public async Task Add(ItemAddViewModel model , string userId)
    {
        try
        {
            var item = new Item()
            {
                Label = model.Label,
                Description = model.Description,
                CategoryId = Guid.Parse(model.Category),
                Price = model.Price,
                OwnerId = userId,
            };
            var images = new List<ItemImages>();
            foreach (var image in model.Images  )
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    image.OpenReadStream().CopyTo(ms);

                    bytes = ms.ToArray();

                }

                var  img = new ItemImages()
                {
                    ItemId = item.Id,
                    Source = bytes
                };

                images.Add(img);
                await this.repository.AddAsync(img);

            }


            await this.repository.AddAsync(item);

            await this.repository.SaveChangesAsync();
        }
        catch 
        {
           
        }
    }

    public Task Edit(ItemAddViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteItem(Guid id)
    {
        var item = await this.repository.GetByIdAsync<Item>(id);
        if (item == null)
        {
            return false;
        }

        this.repository.Delete<Item>(item);
        await this.repository.SaveChangesAsync();

        return true;
    }

}