using System.Security.Cryptography.X509Certificates;
using BestPlace.Core.Contracts;
using BestPlace.Core.Models.Roles;
using BestPlace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Core.Services;

public class RoleService:IRoleService
{
    private readonly RoleManager<IdentityRole> roleManager;

    private readonly UserManager<ApplicationUser> userManager;

    public RoleService(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public async Task<IEnumerable<RoleListViewModel>> All()
    {
        return await this.roleManager.Roles.Select(x => new RoleListViewModel()
        {
            Name = x.Name
        }).ToListAsync();
    }

    public async Task AddRole(string name)
    {
      await  this.roleManager.CreateAsync(new IdentityRole()
        {
            Name = name
        });
    }
}