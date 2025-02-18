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

public class MaintenanceRepository : BaseRepository<Maintenance>, IMaintenanceRepository
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

    public async Task<Maintenance?> FindByIdAsync(
      int id,
      bool includeProducts = true,
      bool includeDevice = true,
      bool includeUser = true)
    {
       
        var query = _context.Maintenances.AsQueryable();
        System.Console.WriteLine("Proxima linha busca por manutenções e carrega o stock");
        if (includeProducts)
        {
            query = query
                .Include(x => x.MaintenanceProducts)
                    .ThenInclude(xp => xp.Stock);
                    System.Console.WriteLine("Fim da consulta de produtos e estoque");
        }
        System.Console.WriteLine("Proxima consulta é por dispositivos!");
        if (includeDevice)
        {
            query = query
                .Include(x => x.Device)
                    .ThenInclude(d => d.Sector);
            System.Console.WriteLine("Fim da consulta por dispositivos");
        }
        System.Console.WriteLine("Proxima consulta é por Usuarios!");
        if (includeUser)
        {
            query = query.Include(x => x.ApplicationUser);
            System.Console.WriteLine("Fim da consulta por usuarios");
        }
        System.Console.WriteLine(query);
        System.Console.WriteLine("Iniciando retorno da query");
        return await query.FirstOrDefaultAsync(x => x.Id == id);
        

        
    }
    
    public async Task InsertAsync(Maintenance maintenance)
    {
        
        if (maintenance.MaintenanceProducts == null || !maintenance.MaintenanceProducts.Any())
        {
            throw new Exception("Nenhum produto foi associdado a manutenção");
        }
        var control = await _context.MaintenanceNumberControls.FirstOrDefaultAsync();
        if (control == null)
        {
            control = new MaintenanceNumberControl { CurrentNumber = 99 };
           await _context.MaintenanceNumberControls.AddAsync(control);
           
        }
        control.CurrentNumber += 1;
        maintenance.MaintenanceNumber = control.CurrentNumber;
       await _context.AddAsync(maintenance);
    }

    public async Task RemoveAsync(int id)
    {
        
        var obj = await _context.Maintenances.FindAsync(id);
        _context.Remove(obj);
        

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
            System.Console.WriteLine("$$$#########################################");
            System.Console.WriteLine(existingProduct);
            if (!maintenance.MaintenanceProducts.Any(p => p.StockId == existingProduct.StockId)
                || existingProduct.QuantityUsed <=0 )
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

    public async Task FinalizeAsync(int id)
    {
      
        var maintenance = await _context.Maintenances.FindAsync(id);
        if (maintenance == null)
        {
            throw new NotFoundException("Id não encontrado1");
        }
        var final = MaintenanceStatus.Finalizada;
        maintenance.Status = final;
        maintenance.CloseDate = DateTime.Now;

        _context.Maintenances.Update(maintenance);
       
    }

    public async Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetMaintenancesAsync(
       int start,
       int length,
       string searchValue,
       string sortColumn,
       string sortDirection)
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
                User = x.ApplicationUser.Name,
                Identifier = x.Device.Identifier,
                SerialNumber = x.Device.SerialNumber,
                DeviceId = x.DeviceId//
            })
            .ToListAsync();

        var totalRecords = await _context.Maintenances.CountAsync();

        return (data, totalRecords, filteredCount);
    }

    
    public async Task<bool> ExistsAsync(int id)
    {
       
        return await _context.Maintenances.AnyAsync(x => x.Id == id);
    }
    


}