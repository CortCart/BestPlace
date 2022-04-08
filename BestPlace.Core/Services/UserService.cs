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
    public async Task<IEnumerable<UserListViewModel>> GetAllUsers()
    {
        var users = this.repository.All<ApplicationUser>();


        return await    users.Select(x => new UserListViewModel()
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Email = x.Email,
                Address = x.Address,
                Phone = x.Phone,

            }).ToListAsync();

        
    }

    public async Task<UserDetailsViewModel> GetUserDetails(string id)
    {
        var user = await this.repository.All<ApplicationUser>().Include(x=>x.Image).Include(x=>x.MyItems).FirstOrDefaultAsync(x=>x.Id==id);
        if (user == null) throw new ArgumentException("Unknown  user");
    
      

        var userRoles = await userManager.GetRolesAsync(user);
        var items = user.MyItems.Select(x => new UserItemDetailsViewModel
        {
            Id = x.Id,
            Name = x.Label
        }).ToList();

        var imgId = user.Image == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : user.Image.Id;

        return new UserDetailsViewModel()
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            Items = items,
            Roles = userRoles,
            ImageId =imgId
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
        var user = await this.repository.GetByIdAsync<ApplicationUser>(id);
        if (user == null) throw new ArgumentException("Unknown user");

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