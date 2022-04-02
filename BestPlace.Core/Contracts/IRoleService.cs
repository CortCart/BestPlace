using BestPlace.Core.Models.Roles;

namespace BestPlace.Core.Contracts;

public interface IRoleService
{
    Task<IEnumerable<RoleListViewModel>> All();

    Task AddRole(string name);
}