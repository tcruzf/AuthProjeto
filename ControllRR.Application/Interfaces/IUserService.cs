using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface IUserService
{
     Task<List<UserDto>> FindAllAsync();
    Task<UserDto> FindByIdAsync(int id);
    Task InsertAsync(UserDto user);
    Task RemoveAsync(int id);
    
}