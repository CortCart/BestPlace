using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Core.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserListViewModel>> GetUsers();

    Task<UserDetailsViewModel> GetUserDetails( string id);

    Task<ApplicationUser> GetUserById(string id);

    Task<bool>  DeleteUser(string id);
}