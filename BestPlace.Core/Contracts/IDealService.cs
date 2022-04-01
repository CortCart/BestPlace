using BestPlace.Core.Models.Deal;

namespace BestPlace.Core.Contracts;

public interface IDealService
{
    Task<IEnumerable<DealListViewModel>> All();

    Task<DealDetailsViewModel> GetDealDetails(Guid id);
}