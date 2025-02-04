/*
    Classe UserRepository
    Lida com as operações de inserção, alteração e remoção de usuarios no banco de dados
*/

using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
public class UserRepository : IUserRepository
{
    private readonly ControllRRContext _controllRRContext;

    public UserRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<List<ApplicationUser>> FindAllAsync()
    {
        return await _controllRRContext.ApplicationUsers
        .Include(x => x.Maintenances)
        .ToListAsync();
    }

    public async Task<ApplicationUser> FindByIdAsync(int id)
    {

        return await _controllRRContext.ApplicationUsers
        .Include(x => x.Maintenances)
        .FirstOrDefaultAsync(x => x.OperatorId == id);

    }

    public async Task InsertAsync(ApplicationUser user)
    {
        _controllRRContext.AddAsync(user);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _controllRRContext.ApplicationUsers
        .Include(x => x.Maintenances)
        .FirstOrDefaultAsync(u => u.OperatorId == id);

        _controllRRContext.Remove(obj);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        bool hasAny = await _controllRRContext.ApplicationUsers.AnyAsync(x => x.Id == user.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id Não encontrado!");
        }
        try
        {
            _controllRRContext.Update(user);
            await _controllRRContext.SaveChangesAsync();
        }
        catch (DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }


    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

}