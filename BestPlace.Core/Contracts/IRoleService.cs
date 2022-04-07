using BestPlace.Core.Models.Roles;

namespace BestPlace.Core.Contracts;

public interface IRoleService
{
    Task<IEnumerable<RoleListViewModel>> GetAllRoles();

    Task<bool> AddRole(string name);
}