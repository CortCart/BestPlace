using BestPlace.Core.Contracts;
using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class UserService:IUserService
{
    private readonly IApplicatioDbRepository repository;

    public UserService(IApplicatioDbRepository repository)
    {
        this.repository = repository;
    }
    public async Task<IEnumerable<UserListViewModel>> GetUsers()
    {
        var users = this.repository.All<ApplicationUser>()
            .Select(x => new UserListViewModel()
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Email = x.Email,
                Address = x.Address,
                Phone = x.Phone,

            }).ToListAsync();

        return await users;
    }

    public async Task<UserDetailsViewModel> GetUserDetails(string id)
    {
        var user = await this.GetUserById(id);

        var dealAsOwner = await this.repository.All<Deal>()
            .Where(x => x.ExOwnerId == id)
            .Select(x => new UserDealAsOwnerViewModel()
            {
                BuyerId = x.BuyerUserId,
                BuyerName = $"{x.BuyerUser.FirstName} {x.BuyerUser.LastName}",
                ItemId = x.ItemId,
                ItemName = x.Item.Label
            })
            .ToListAsync();

        var dealAsBuyer = await this.repository.All<Deal>()
            .Where(x => x.ExOwnerId == id)
            .Select(x => new UserDealAsBuyerViewModel()
            {
                ExOwnerId = x.ExOwnerId,
                ExOwnerName = $"{x.ExOwner.FirstName} {x.ExOwner.LastName}",
                ItemId = x.ItemId,
                ItemName = x.Item.Label
            })
            .ToListAsync();

        return new UserDetailsViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            DealsAsBuyer = dealAsBuyer,
            DealsAsOwner = dealAsOwner
        };
    }

    //public async Task<UserEditViewModel> GetUserForEdit(string id)
    //{
    //    var user = await this.repository.GetByIdAsync<ApplicationUser>(id);

    //    return new UserEditViewModel()
    //    {
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        Email = user.Email,
    //        Address = user.Address,
    //        Phone = user.Phone
    //    };
    //}

    //public async Task<bool> UpdateUser(UserEditViewModel model)
    //{
    //    var user = await this.repository.GetByIdAsync<ApplicationUser>(model.Id);
    //    if (user == null)
    //    {
    //        return false;
    //    }
    //    user.FirstName = model.FirstName;
    //    user.LastName= model.LastName;
    //    user.Email = model.Email;
    //    user.Address = model.Address;
    //    user.Phone = model.Phone;

    //  await   this.repository.SaveChangesAsync();
    //    return true;
    //}

    public async Task<ApplicationUser> GetUserById(string id)
    {
        return await this.repository.GetByIdAsync<ApplicationUser>(id);
    }

    public async Task<bool> DeleteUser(string id)
    {
        var user = await this.repository.GetByIdAsync<ApplicationUser>(id);
        if (user == null)
        {
            return false;
        }

          this.repository.Delete<ApplicationUser>(user);
        await  this.repository.SaveChangesAsync();

        return true;
    }


}