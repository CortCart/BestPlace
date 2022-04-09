using BestPlace.Core.Models.Call;

namespace BestPlace.Core.Constants;

public interface ICallService
{
    Task<IEnumerable<CallListViewModel>> All();

    Task<bool> AddCall(CallAddViewModel model, string userId);

    Task<CallDetailsViewModel> GetCallDetails(Guid id);

    Task DeleteCall(Guid id);
}