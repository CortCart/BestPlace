using BestPlace.Core.Contracts;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;

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
        var category = await this.repository.GetByIdAsync<Category>(id);
        if (category == null) throw new ArgumentException("Unknown category");
        return category.Image;
    }

    public async Task<byte[]> GetItemImage(Guid id)
    {
        var itemImage = await this.repository.GetByIdAsync<ItemImages>(id);
        if (itemImage == null) throw new ArgumentException("Unknown image");

        return itemImage.Source;
    }

    public async Task DeleteItemImage(Guid id)
    {
        var image = await this.repository.GetByIdAsync<ItemImage>(id);
        if (image == null) throw new ArgumentException("Unknown image");
        this.repository.Delete(image);
        await this.repository.SaveChangesAsync();
    }
}