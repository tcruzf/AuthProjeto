/*
    Classe DeviceRepository
    Lida com a inserção, consulta e remoção de dispositivos do sistema


*/

using System.Linq.Dynamic.Core;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
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

    
    /// O metodo abaixo retorna uma lista de dispositivos, inclue os setores quais estão localizados e as manutenções abertas
    ///  e relacionadas a ele
    ///  Retorna lista de devices(dispositivos) 
    public async Task<List<Device>> FindAllAsync()
    {
        return await _controllRRContext.Devices
        .Include(x => x.Sector)
        .Include(x => x.Maintenances)
        .ToListAsync();
    }

    // Busca um dispositivo com base em um inteiro(Id)   
    public async Task<Device?> FindByIdAsync(int id)
    {
        return await _controllRRContext.Devices
        .Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    // Busca informações sobre um determinado dispositivo com base no seu Id.
    // Inclue na busca todas as manutenções relacionadas a ele e também seu setor
    public async Task<Device?> GetMaintenancesAsync(int id)
    {
        return await _controllRRContext.Devices
        .Include(x => x.Maintenances)
        .Include(x => x.Sector)
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    // Cria um novo dispositivo
    public async Task InsertAsync(Device device)
    {
        await _controllRRContext.AddAsync(device);
        await _controllRRContext.SaveChangesAsync();

    }

    // Realiza a persistencia de informações alteradas de um determinado dispositivo
    public async Task UpdateAsync(Device device)
    {
        // Verica se o dispositivo existe (com base no Id) no banco de dados
        bool existDevice = await _controllRRContext.Devices.AnyAsync(x => x.Id == device.Id);

        // Caso não exista, lança a exception
        if (!existDevice)
        {
            throw new NotFoundException("Objeto não encontrado");

        }
        // Tenta realizar o update com base nas informações vindas do service, caso não tenha nenhuma informação
        // invalida, então realiza a persistencia dos novos dados no banco de dados.
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

    // Realiza a persistencia dos dados.
    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

    // Metodo utilizado para busca dinamica de informações através de datatables
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