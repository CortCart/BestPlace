using Microsoft.EntityFrameworkCore;
using BestPlace.Core.Constants;
using BestPlace.Core.Models.Call;
using BestPlace.Core.Models.Category;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class CallService:ICallService
{
    private readonly IApplicatioDbRepository repository;

    public CallService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<CallListViewModel>> All()
    {
        var calls = await this.repository.All<Call>()
            .Select(x => new CallListViewModel()
            {
                Id = x.Id,
                Email = x.User.Email
            }).ToListAsync();

        return calls;
    }

    public async Task<bool> AddCall(CallAddViewModel model, string userId)
    {
        try
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
            var call = new Call()
            {
                Problem = model.Problem,
                UserId = userId
            };
            await this.repository.AddAsync(call);
            await this.repository.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public  async  Task<CallDetailsViewModel> GetCallDetails(Guid id)
    {
        var call = await this.repository.All<Call>().Include( x=>x.User).FirstOrDefaultAsync(x => x.Id == id);
        if (call == null) throw new ArgumentException("Unknow call");

        return new CallDetailsViewModel()
        {
            Id = call.Id,
            Email = call.User.Email,
            Name = $"{call.User.FirstName} {call.User.LastName}",
            Problem = call.Problem,
            UserId = call.UserId
        };
    }

    public async Task DeleteCall(Guid id)
    {
        var call = await this.repository.GetByIdAsync<Call>(id);
        if (call == null) throw new ArgumentException("Unknow call");

        this.repository.Delete(call);
        await this.repository.SaveChangesAsync();
    }
}