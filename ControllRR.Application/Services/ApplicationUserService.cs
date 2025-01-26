/*
    Em desenvolvimento
    Classe ApplicationUserService
    Responsavel pelos serviços relacionados aos logins de usuarios.
*/
using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Exceptions;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Application.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IUserRepository _userRepository;

    public ApplicationUserService(UserManager<ApplicationUser> userManager, IMapper mapper, IApplicationUserRepository applicationUserRepository, IUserRepository userRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _applicationUserRepository = applicationUserRepository;
        _userRepository = userRepository;
    }

     public async Task<bool> AddUserRoleAsync(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false; // Usuário não encontrado
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<List<ApplicationUserDto>> FindAllAsync()
    {
        var user = await  _applicationUserRepository.FindAllAsync();
        return _mapper.Map<List<ApplicationUserDto>>(user);
    }

    public async Task<ApplicationUser?> GetUserManagerAsync(string email)
    {
       return  await _userManager.FindByEmailAsync(email);
        
    }

   
}