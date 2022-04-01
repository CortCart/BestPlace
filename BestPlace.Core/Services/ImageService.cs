using BestPlace.Core.Contracts;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;

namespace BestPlace.Core.Services;

public class ImageService:IImageService
{
    private readonly IApplicatioDbRepository repository;

    public ImageService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<byte[]> GetCategoryImage(Guid id)
    {
        var image = await this.repository.GetByIdAsync<Category>(id);
        return image.Image;
    }

    public async Task<byte[]> GetItemImage(Guid id)
    {
        var image = await this.repository.GetByIdAsync<ItemImages>(id);
        return  image.Source;
    }
}