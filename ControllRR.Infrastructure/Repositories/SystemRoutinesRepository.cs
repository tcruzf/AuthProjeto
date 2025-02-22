using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;

namespace ControllRR.Infrastructure.Repositories;

public class SystemRoutinesRepository : BaseRepository<SystemRoutine>, ISystemRoutinesRepository
{
    public SystemRoutinesRepository(ControllRRContext context) : base(context)
    {

    }

    public async Task CreateConfigAsync(SystemRoutine systemRoutine)
    {
        var hasAny = await _context.SystemRoutines.ToListAsync();
        if (hasAny.Any())
            throw new Exception("Já existe uma configuração para esta instancia! ");

        await _context.SystemRoutines.AddAsync(systemRoutine);

    }

    public async Task<SystemRoutine> FindConfigAsync(int id)
    {
        try
        {
            var result = await _context.SystemRoutines.FirstOrDefaultAsync(x => x.Id == id);
            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateConfigAsync(SystemRoutine systemRoutine)
    {
        var result = await _context.SystemRoutines.FirstOrDefaultAsync(x => x.Id == systemRoutine.Id);
        if (result == null)
            throw new Exception("Nenhum registro de configuração foi encontrado");

        _context.Entry(systemRoutine).CurrentValues.SetValues(result);


    }
}