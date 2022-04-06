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

    public async Task<ItemQueryViewModel> AllPublic(Guid categoryId, string query)
    {
        var items = await this.repository.All<Item>()
            .Where(x=>x.CategoryId==categoryId)
            .Where(x=>!x.IsBought)
            .Select(x => new ItemPublicListViewModel()
            {
                Id = x.Id,
                Label = x.Label,
               Price = x.Price,
               Image = x.Images.Select(x=> new ItemImageDetailsViewModel
               {
                   Id = x.Id,
               }).First()
            }).ToListAsync();

        if (query != null)
        {

            return  new ItemQueryViewModel
          {
              CategoryId = categoryId,
              Query = query,
              Items = items.Where(x => x.Label.ToLower().Contains(query.ToLower()))
        };

         
        }

        return  new ItemQueryViewModel
        {
            CategoryId = categoryId,
            Query = query,
            Items = items
        };
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
        var item = await this.repository.All<Item>().Include(x=>x.Images).Include(x=>x.Owner ).Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);
        if (item == null) throw new ArgumentException("Unknown item");


          var itemDetails =  new ItemDetailsViewModel
            {
                Id = item.Id,
                Label = item.Label,
                Description = item.Description,
                IsBought = item.IsBought,
                Likes = item.Likes,
                Price = item.Price,
                OwnerId = item.OwnerId,
                OwnerName = $"{item.Owner.FirstName} {item.Owner.LastName}",
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                Images = item.Images.Select(y=>new ItemImageDetailsViewModel
                {
                   Id = y.Id,
                }).ToList()
            };
          itemDetails.BuyerId = await this.repository.All<Deal>().Where(x=>x.ItemId==item.Id).Select(x=>x.BuyerUserId).FirstOrDefaultAsync();
          itemDetails.BuyerName = await this.repository.All<Deal>().Where(x => x.ItemId == item.Id).Select(x => $"{x.BuyerUser.FirstName} {x.BuyerUser.LastName}").FirstOrDefaultAsync();
        return itemDetails;
    }

    public async Task Add(ItemAddViewModel model , string userId)
    {
        var item = new Item()
        {
            Label = model.Label,
            Description = model.Description,
            Price = model.Price,
            CategoryId = Guid.Parse(model.Category),
            OwnerId = userId
        };

        var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
       var category = await this.repository.GetByIdAsync<Category>(Guid.Parse(model.Category));

       user.MyItems.Add(item);
       category.Items.Add(item);

       item.Owner = user;
       item.Category = category;

            var images = new List<ItemImages>();
            foreach (var image in model.Images  )
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {

                  await  image.OpenReadStream().CopyToAsync(ms);

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


          await   this.repository.AddAsync(item);

        await     this.repository.SaveChangesAsync();
            
    }

    public Task Edit(ItemAddViewModel model)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteItem(Guid id)
    {
        var item = await this.repository.GetByIdAsync<Item>(id);
        if (item == null) throw new ArgumentException("Unknown item");

        this.repository.Delete<Item>(item);
        await this.repository.SaveChangesAsync();
    }

}