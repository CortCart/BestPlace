using BestPlace.Infrastructure.Data;

namespace BestPlace.Core.Contracts;

public interface IImageService
{
    Task<byte[]> GetCategoryImage(Guid id);

    Task<byte[]> GetItemImage(Guid id);

    Task DeleteItemImage(Guid id);

    Task AddImageProfile(Image image ,string userId);
}