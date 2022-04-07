﻿using BestPlace.Core.Models.User;
using BestPlace.Infrastructure.Data.Identity;

namespace BestPlace.Core.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserListViewModel>> GetAllUsers();

    Task<UserDetailsViewModel> GetUserDetails( string id);

    Task<ApplicationUser> GetUserById(string id);

    Task  DeleteUser(string id);
}