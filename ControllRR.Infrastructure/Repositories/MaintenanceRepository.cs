using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
using ControllRR.Domain.Enums;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ControllRRContext _controllRRContext;

    public MaintenanceRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task<List<Maintenance>> FindAllAsync()
    {

        return await _controllRRContext.Maintenances
        .Include(x => x.User)
        .ToListAsync();
    }

    public async Task<Maintenance> FindByIdAsync(int id)
    {
        return await _controllRRContext.Maintenances
            .Include(x => x.User)
            .Include(x => x.Device)
             .Include(x => x.Device.Sector)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task InsertAsync(Maintenance maintenance)
    {
        var control = await _controllRRContext.MaintenanceNumberControls.FirstOrDefaultAsync();
        if (control == null)
        {
            control = new MaintenanceNumberControl { CurrentNumber = 99 };
            _controllRRContext.MaintenanceNumberControls.Add(control);
            await _controllRRContext.SaveChangesAsync();
        }
        control.CurrentNumber += 1;
        maintenance.MaintenanceNumber = control.CurrentNumber;
        _controllRRContext.Update(control);
        _controllRRContext.Add(maintenance);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _controllRRContext.Maintenances.FindAsync(id);
        _controllRRContext.Remove(obj);
        await _controllRRContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Maintenance maintenance)
    {
        bool hasAny = await _controllRRContext.Maintenances.AnyAsync(x => x.Id == maintenance.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id Não encontrado!");
        }
        try
        {
            _controllRRContext.Update(maintenance);
            await _controllRRContext.SaveChangesAsync();

        }
        catch (DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public async Task FinalizeAsync(int id)
    {
        var maintenance = await _controllRRContext.Maintenances.FindAsync(id);
        if (maintenance == null)
        {
            throw new NotFoundException("Id não encontrado1");
        }
        var final = MaintenanceStatus.Finalizada;
        maintenance.Status = final;
        maintenance.CloseDate = DateTime.Now;

        _controllRRContext.Maintenances.Update(maintenance);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetMaintenancesAsync(
       int start,
       int length,
       string searchValue,
       string sortColumn,
       string sortDirection)
    {
        var query = _controllRRContext.Maintenances
            .Include(x => x.Device)
            .Include(x => x.User)
            .AsQueryable();

        // Filtragem
        if (!string.IsNullOrEmpty(searchValue))
        {   //Gambiarra para poder fazer uma porrada de tentativa de pegar um ou outro valor(não vou explicar, tô com a cabeça e o estomago doendo e sem paciencia!)
            query = query.Where(x =>
                (x.SimpleDesc != null && x.SimpleDesc.ToLower().Contains(searchValue)) ||
                (x.Device.SerialNumber != null && x.Device.SerialNumber.ToLower().Contains(searchValue)) ||
                (x.Description != null && x.Description.ToLower().Contains(searchValue)) ||
                (x.Device != null && x.Device.Model != null && x.Device.Model.ToLower().Contains(searchValue)) ||
                (x.User != null && x.User.Name != null && x.User.Name.ToLower().Contains(searchValue)) ||
                (x.Device != null && x.Device.Identifier != null && x.Device.Identifier.ToLower().Contains(searchValue)));
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
                SimpleDesc = x.SimpleDesc,
                Status = x.Status,
                MaintenanceNumber = x.MaintenanceNumber,
                Description = x.Description,
                Device = x.Device.Model,
                User = x.User.Name,
                Identifier = x.Device.Identifier,
                SerialNumber = x.Device.SerialNumber,
                DeviceId = x.DeviceId//
            })
            .ToListAsync();

        var totalRecords = await _controllRRContext.Maintenances.CountAsync();

        return (data, totalRecords, filteredCount);
    }
    

    
    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }


}