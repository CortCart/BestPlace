using BestPlace.Core.Contracts;
using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class UserService:IUserService
{

    private readonly UserManager<ApplicationUser> userManager;


    private readonly IApplicatioDbRepository repository;

    public UserService(IApplicatioDbRepository repository, UserManager<ApplicationUser> userManager)
    {
        this.repository = repository;
        this.userManager = userManager;
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
        if (user == null) throw new ArgumentException("Unknown  user");
    
      

        var userRoles = await userManager.GetRolesAsync(user);


        return new UserDetailsViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            Roles = userRoles
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


    public async Task<ApplicationUser> GetUserById(string id)
    {
        return await this.repository.GetByIdAsync<ApplicationUser>(id);
    }

    public async Task DeleteUser(string id)
    {
        var user = await this.repository.GetByIdAsync<ApplicationUser>(id);
        if (user == null) throw new ArgumentException("Unknown user");


        this.repository.Delete<ApplicationUser>(user);
        await  this.repository.SaveChangesAsync();

    }


}