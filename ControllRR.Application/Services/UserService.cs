/*
    Classe UserService
    Responsavel por lidar com todos os serviços relacionados a usuarios
*/
using AutoMapper;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Application.Dto;
using ControllRR.Application.Exceptions;

namespace ControllRR.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<List<ApplicationUserDto>> FindAllAsync()
    {
        var user = await _userRepository.FindAllAsync();
        return _mapper.Map<List<ApplicationUserDto>>(user);

    }

    public async Task<ApplicationUserDto> FindByIdAsync(int id)
    {

        var user = await _userRepository.FindByIdAsync(id);
        return _mapper.Map<ApplicationUserDto>(user);


    }

    /// <summary>
    /// Insere um usuario com base nas informações passadas pelo controller
    /// Recebe um UserDTO do tipo 'DTO' e faz o mapeamento para um objeto do tipo User
    /// Transfere as informações para o repository e faz a chamada para salvar as informações
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    public async Task InsertAsync(ApplicationUserDto userDto)
    {
        var user = _mapper.Map<ApplicationUser>(userDto);

        await _userRepository.InsertAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    /// <summary>
    ///  Remove um usuario com base no id(int) fornecido.
    ///  Caso o usuario tenha manutenções, a remoção não poderá ser concluida.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task RemoveAsync(int id)
    {
        var obj = await _userRepository.FindByIdAsync(id);

        if (obj == null)
        {
            throw new NotFoundException("Usuario não encontrado");
        }

        if (obj.Maintenances != null && obj.Maintenances.Any())
        {
            throw new InvalidOperationException("Não é possivel remover usuario que tenha manutenções registradas!");
        }

        // await _userRepository.SaveChangesAsync();

    }

    public async Task UpdateAsync(ApplicationUserDto userDto)
    {
        var user = _mapper.Map<ApplicationUser>(userDto);
        await _userRepository.UpdateAsync(user);
        //await _userRepository.SaveChangesAsync();
    }

}