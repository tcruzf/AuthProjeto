using System.Linq.Dynamic.Core;
using ControllRR.Infrastructure.Data.Context;

using ControllRR.Domain.Entities;

//using ControlRR.Services.Exceptions;
//using ControlRR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
using Mysqlx.Expr;

namespace ControllRR.Infrastructure.Repositories;
public class DeviceRepository : IDeviceRepository
{
    private readonly ControllRRContext _controllRRContext;

    public DeviceRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }
    public async Task<List<Device>> FindAllAsync()
    {
        return await _controllRRContext.Devices
        .Include(x => x.Sector)
        .Include(x => x.Maintenances)
        .ToListAsync();
    }

    public async Task<Device> FindByIdAsync(int id)
    {
        return await _controllRRContext.Devices
        .Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Device> GetMaintenancesAsync(int id)
    {
        return await _controllRRContext.Devices
        .Include(x => x.Maintenances)
        .Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Device device)
    {
        await _controllRRContext.AddAsync(device);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Device device)
    {
        bool existDevice = await _controllRRContext.Devices.AnyAsync(x => x.Id == device.Id);

        if (!existDevice)
        {
            throw new NotFoundException("Objeto não encontrado");

        }
        try
        {
            _controllRRContext.Devices.Update(device);
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

     public async Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetDevicesAsync(
       int start,
       int length,
       string searchValue,
       string sortColumn,
       string sortDirection)
    {
        var query = _controllRRContext.Devices
            .Include(x => x.Maintenances)
            .Include(x => x.Sector)
            .AsQueryable();

        // Filtragem
        if (!string.IsNullOrEmpty(searchValue))
        {   //Gambiarra para poder fazer uma porrada de tentativa de pegar um ou outro valor(não vou explicar, tô com a cabeça e o estomago doendo e sem paciencia!)
            query = query.Where(x =>
                (x.DeviceDescription != null && x.DeviceDescription.ToLower().Contains(searchValue)) ||
                (x.Model != null && x.Model.ToLower().Contains(searchValue)) ||
                (x.Type != null && x.Type.ToLower().Contains(searchValue)) ||
                (x.Identifier != null &&  x.Identifier.ToLower().Contains(searchValue)) ||
                (x.SerialNumber != null && x.SerialNumber != null && x.SerialNumber.ToLower().Contains(searchValue)));
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
                Type = x.Type,
                Identifier = x.Identifier,
                Model = x.Model,
                Description = x.DeviceDescription,
                SerialNumber = x.SerialNumber,
                Sector = x.Sector.Name,
                DeviceId = x.Id,
                DeviceDescription = x.DeviceDescription
                
            })
            .ToListAsync();

        var totalRecords = await _controllRRContext.Devices.CountAsync();

        return (data, totalRecords, filteredCount);
    }
    

}