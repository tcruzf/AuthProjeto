using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Application.Interfaces;

public interface IApplicationUserService
{
     Task<bool> AddUserRoleAsync(string email, string role);

     Task<ApplicationUser?> GetUserManagerAsync(string email);

      Task<List<ApplicationUserDto>> FindAllAsync();
}