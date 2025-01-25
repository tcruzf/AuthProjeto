using System.Linq.Dynamic.Core;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;

public class SectorRepository : ISectorRepository
{
    private readonly ControllRRContext _controllRRContext;
    public SectorRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task<List<Sector>> FindAllAsync()
    {
        return await _controllRRContext.Sectors
        .Include(x => x.Devices)
        .ToListAsync();
    }

    public async Task InsertAsync(Sector sector)
    {
        _controllRRContext.Add(sector);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task<Sector> FindByIdAsync(int id)
    {
        return await _controllRRContext.Sectors
        .Include(x => x.Devices)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

     public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

     public async Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetSectorAsync(
       int start,
       int length,
       string searchValue,
       string sortColumn,
       string sortDirection)
    {
        var query = _controllRRContext.Sectors
            .Include(x => x.Devices)
            .AsQueryable();

        // Filtragem
        if (!string.IsNullOrEmpty(searchValue))
        {   //Gambiarra para poder fazer uma porrada de tentativa de pegar um ou outro valor(não vou explicar, tô com a cabeça e o estomago doendo e sem paciencia!)
            query = query.Where(x =>
                (x.Cep != null && x.Cep.ToLower().Contains(searchValue)) ||
                (x.Address != null && x.Address.ToLower().Contains(searchValue)) ||
                (x.City != null && x.City.ToLower().Contains(searchValue)) ||
                (x.Name != null && x.Name.ToLower().Contains(searchValue)) ||
                (x.Location != null && x.Location.ToLower().Contains(searchValue)) ||
                (x.RequesterName != null &&  x.RequesterName.ToLower().Contains(searchValue)) ||
                (x.Neighborhood != null && x.Neighborhood != null && x.Neighborhood.ToLower().Contains(searchValue)));
        }

        // Contagem após o filtro
        var filteredCount = await query.CountAsync();

        // Ordenação
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection))
        {
            query = query.OrderBy($"{sortColumn} {sortDirection}");
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }

        // Paginação
        var data = await query
            .Skip(start)
            .Take(length)
            .Select(x => new
            {
                Id = x.Id,
                Cep = x.Cep,
                Address = x.Address,
                Location = x.Location,
                Name = x.Name,
                RequesterName = x.RequesterName,
                Neighborhood = x.Neighborhood
                
            })
            .ToListAsync();

        var totalRecords = await _controllRRContext.Devices.CountAsync();

        return (data, totalRecords, filteredCount);
    }

  
}