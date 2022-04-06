using BestPlace.Core.Contracts;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class ImageService : IImageService
{
    private readonly IApplicatioDbRepository repository;

    public ImageService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<byte[]> GetCategoryImage(Guid id)
    {
        var category = await this.repository.All<Category>().Include(x=>x.Image).FirstOrDefaultAsync(x=>x.Id==id);
        if (category == null) throw new ArgumentException("Unknown category");
        return category.Image.Source;
    }

    public async Task<byte[]> GetItemImage(Guid id)
    {
        var itemImage = await this.repository.All<ItemImages>().Include(x=>x.Item).Include(x=>x.Image).FirstOrDefaultAsync(x=>x.Id==id);
        if (itemImage == null) throw new ArgumentException("Unknown image");

        return itemImage.Image.Source;
    }

    public async Task DeleteItemImage(Guid id)
    {
        var itemImage = await this.repository.All<ItemImages>().Include(x => x.Item).Include(x => x.Image).FirstOrDefaultAsync(x => x.Id == id);
        if (itemImage == null) throw new ArgumentException("Unknown image");
        this.repository.Delete(itemImage);
        await this.repository.SaveChangesAsync();
    }
}