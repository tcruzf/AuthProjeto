using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ControllRRContext _controllRRContext;

    public ApplicationUserRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<List<ApplicationUser>> FindAllAsync()
    {
        var query = @"SELECT *
                  FROM `AspNetRoles`
                  "; // Ajuste o valor se necessário
        var execution = await _controllRRContext.ApplicationUsers
            .FromSqlRaw(query)
            .AsNoTracking()
            .ToListAsync();
        return execution;
      
    }

    public async Task<ApplicationUser> FindByIdAsync(int id)
    {   
        

        return await _controllRRContext.ApplicationUsers
        .FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task InsertAsync(ApplicationUser applicationUser)
    {
        _controllRRContext.AddAsync(applicationUser);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _controllRRContext.ApplicationUsers
      
        .FirstOrDefaultAsync(u => u.Id == id);

        _controllRRContext.Remove(obj);
        await _controllRRContext.SaveChangesAsync();

    }



    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

}