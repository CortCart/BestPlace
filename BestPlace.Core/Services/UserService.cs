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
        var user = await this.repository.All<ApplicationUser>().Include(x=>x.MyItems).Include(x=>x.Image).Include(x=>x.MyItems).FirstOrDefaultAsync(x=>x.Id==id);
        if (user == null) throw new ArgumentException("Unknown  user");



        var items = new List<UserItemDetailsWithImageViewModel>();
        foreach (var item in user.MyItems)
        {
            var info = await this.repository.All<Item>().Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == item.Id);

            var itemDetails = new UserItemDetailsWithImageViewModel
            {
                Id = info.Id,
                Name = info.Label,
                ImageId = info.Images.First().Id
            };
            items.Add(itemDetails);
        }
          
        

        var imgId = user.Image == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : user.Image.Id;

        return new UserDetailsViewModel()
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            InstagramUrl = user.Instagram,
            Facebook = user.Facebook,
            Items = items,
            ImageId =imgId
        };
    }

    public async Task<UserDetailsViewModelAsAdmin> GetUserDetailsAsAdmin(string id)
    {
        var user = await this.repository.All<ApplicationUser>().Include(x => x.Image).Include(x => x.MyItems).FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new ArgumentException("Unknown  user");



        var userRoles = await userManager.GetRolesAsync(user);
        var items = user.MyItems.Select(x => new UserItemDetailsViewModel
        {
            Id = x.Id,
            Name = x.Label
        }).ToList();

        var imgId = user.Image == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : user.Image.Id;

        return new UserDetailsViewModelAsAdmin()
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            FacebookUrl = user.Facebook,
            InstagramUrl = user.Instagram,
            Items = items,
            Roles = userRoles,
            ImageId = imgId
        };
    }

    public async Task<UserEditViewModel> GetUserForEdit(string id)
    {
        var user = await this.repository.GetByIdAsync<ApplicationUser>(id);

        return new UserEditViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            Phone = user.Phone,
            FacebookUrl = user.Facebook,
            InstagramUrl = user.Instagram
        };
    }

    public async Task<bool> EditUser(UserEditViewModel model, string userId)
    {
        var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);

        try
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.Facebook = model.FacebookUrl;
            user.Instagram = model.InstagramUrl;
            if (model.Image != null)
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {

                    await model.Image.OpenReadStream().CopyToAsync(ms);

                    bytes = ms.ToArray();

                }
                if(user.Image!=null) this.repository.Delete(user.Image);


                var image = new Image()
                {
                    Source = bytes
                };
                user.Image = image;
                user.ImageId = image.Id;
                await this.repository.AddAsync(image);
            }

            await this.repository.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

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