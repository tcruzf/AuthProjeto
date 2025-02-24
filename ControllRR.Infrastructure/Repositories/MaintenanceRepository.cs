/*
    Classe MaintenanceRepository
    Lida com as operações de inserção, alteração e remoção das manutenções no banco de dados
*/
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
using ControllRR.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using ControllRR.Infrastructure.Repositories;
using System.Globalization;
using ControllRR.Domain.PaginatedResult;

public class MaintenanceRepository : GenericRepository<Maintenance>, IMaintenanceRepository
{

    public MaintenanceRepository(ControllRRContext context) : base(context)
    {

    }

    public async Task<List<Maintenance>> FindAllAsync()
    {
        return await _context.Maintenances
        .Include(x => x.ApplicationUser)
        .ToListAsync();
    }

    public async Task<Maintenance?> GetByIdWithDetailsAsync(int id, bool includeProducts = true, bool includeDevice = true, bool includeUser = true)
    {
        var query = BuildDetailedQuery(includeProducts, includeDevice, includeUser);
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _context.Maintenances.FindAsync(id);
        if (obj != null)
        {
            _context.Maintenances.Remove(obj);
        }
    }

    public async Task UpdateAsync(Maintenance maintenance)
    {
        var existingMaintenance = await _context.Maintenances
            .Include(m => m.MaintenanceProducts)
            .FirstOrDefaultAsync(m => m.Id == maintenance.Id);
        if (existingMaintenance == null)
            throw new NotFoundException("Manutenção não encontrada...");

        try
        {
            _context.Entry(existingMaintenance).CurrentValues.SetValues(maintenance);

            await UpdateMaintenanceProductsAsync(maintenance);

        }
        catch (DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public async Task UpdateMaintenanceProductsAsync(Maintenance maintenance)
    {

        var existingMaintenance = await _context.Maintenances
            .Include(m => m.MaintenanceProducts)
            .FirstOrDefaultAsync(m => m.Id == maintenance.Id);

        if (existingMaintenance == null) throw new NotFoundException("Manutenção não encontrada");

        // Remove produtos excluídos
        foreach (var existingProduct in existingMaintenance.MaintenanceProducts.ToList())
        {
            if (!maintenance.MaintenanceProducts.Any(p => p.StockId == existingProduct.StockId)
                || existingProduct.QuantityUsed <= 0)
            {
                _context.MaintenanceProduct.Remove(existingProduct);
            }
        }

        // Atualiza/Adiciona produtos
        foreach (var product in maintenance.MaintenanceProducts)
        {
            var existingProduct = existingMaintenance.MaintenanceProducts
                .FirstOrDefault(p => p.StockId == product.StockId);

            if (existingProduct != null)
            {
                existingProduct.QuantityUsed = product.QuantityUsed;
            }
            else
            {
                existingMaintenance.MaintenanceProducts.Add(product);
            }
        }


    }

    public async Task FinalizeMaintenanceAsync(int id)
    {

        var maintenance = await _context.Maintenances.FindAsync(id);
        if (maintenance == null) throw new NotFoundException("Maintenance não encontrada!");

        maintenance.Status = MaintenanceStatus.Finalizada;
        maintenance.CloseDate = DateTime.Now;
        await UpdateAsync(maintenance);
    }

    public async Task<PaginatedResult<object>> GetPaginatedMaintenancesAsync(
       int start,
       int length,
       string searchValue,
       string sortColumn,
       string sortDirection)
    {

        var query = BuildMaintenanceQuery(searchValue);
        var orderedQuery = ApplySorting(query, sortColumn, sortDirection);

        return new PaginatedResult<object>
        {

            Data = await orderedQuery
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
                User = x.ApplicationUser.Name,
                Identifier = x.Device.Identifier,
                SerialNumber = x.Device.SerialNumber,
                DeviceId = x.DeviceId//
            })
            .ToListAsync(),
            TotalRecords = await _context.Maintenances.CountAsync(),
            FilteredRecords = await query.CountAsync()
        };


    }

    public async Task<Dictionary<string, int>> GetMaintenanceCountByMonth()
    {
        return await _context.Maintenances
          .Where(m => m.OpenDate.HasValue) // Filtra registros com data não nula
          .GroupBy(m => m.OpenDate.Value.Month) // Acessa o Month do DateTime garantido
          .Select(g => new
          {
              Month = g.Key,
              Count = g.Count()
          })
          .ToDictionaryAsync(
              k => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(k.Month),
              v => v.Count
          );
    }

    #region Private Helpers
    private IQueryable<Maintenance> BuildDetailedQuery(bool includeProducts, bool includeDevice, bool includeUser)
    {
        var query = _context.Maintenances.AsQueryable();
        if (includeProducts)
            query = query.Include(x => x.MaintenanceProducts).ThenInclude(xp => xp.Stock);

        if (includeDevice)
            query = query.Include(x => x.Device).ThenInclude(d => d.Sector);

        if (includeUser)
            query = query.Include(x => x.ApplicationUser);

        return query;
    }
    private IQueryable<Maintenance> BuildMaintenanceQuery(string searchValue)
    {
        var query = _context.Maintenances
            .Include(x => x.Device)
            .Include(x => x.ApplicationUser)
            .AsQueryable();

        // Filtragem 
        if (!string.IsNullOrEmpty(searchValue))
        {   //Gambiarra para poder fazer uma porrada de tentativa de pegar um ou outro valor(não vou explicar, tô com a cabeça e o estomago doendo e sem paciencia!)
            query = query.Where(x =>
                (x.SimpleDesc != null && x.SimpleDesc.ToLower().Contains(searchValue)) ||
                (x.Device.SerialNumber != null && x.Device.SerialNumber.ToLower().Contains(searchValue)) ||
                (x.Description != null && x.Description.ToLower().Contains(searchValue)) ||
                (x.Device != null && x.Device.Model != null && x.Device.Model.ToLower().Contains(searchValue)) ||
                (x.ApplicationUser != null && x.ApplicationUser.Name != null && x.ApplicationUser.Name.ToLower().Contains(searchValue)) ||
                (x.Device != null && x.Device.Identifier != null && x.Device.Identifier.ToLower().Contains(searchValue)));
        }
        return query;
    }

    private IQueryable<Maintenance> ApplySorting(IQueryable<Maintenance> query, string sortColumn, string sortDirection)
    {
        return !string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDirection)
        ? query.OrderBy($"{sortColumn} {sortDirection}")
        : query = query.OrderBy(x => x.Id);
    }
    #endregion
    public async Task<bool> ExistsAsync(int id)
    {

        return await _context.Maintenances.AnyAsync(x => x.Id == id);
    }

    // Return quantity of maintenances in dabase
    public async Task<int> CountMaintenance()
    {
        return await _context.Maintenances.CountAsync();
    }




}