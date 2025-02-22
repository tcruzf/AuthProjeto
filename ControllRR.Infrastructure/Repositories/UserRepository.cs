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
using ControllRR.Infrastructure.Repositories;
using System.Linq.Dynamic.Core;
public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    

    public UserRepository(ControllRRContext context) : base (context)
    {
        
    }
    public async Task<List<ApplicationUser>> FindAllAsync()
    {
        return await _context.ApplicationUsers
        .Include(x => x.Maintenances)
        .ToListAsync();
    }

    public async Task<ApplicationUser?> FindByIdAsync(int id)
    {

        return await _context.ApplicationUsers
        .Include(x => x.Maintenances)
        .FirstOrDefaultAsync(x => x.OperatorId == id);

    }

    public async Task InsertAsync(ApplicationUser user)
    {
        await _context.AddAsync(user);

    }

    public async Task RemoveAsync(int id)
    {
        var obj = await _context.ApplicationUsers
        .Include(x => x.Maintenances)
        .FirstOrDefaultAsync(u => u.OperatorId == id);

        _context.Remove(obj);


    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        bool hasAny = await _context.ApplicationUsers.AnyAsync(x => x.Id == user.Id);
        if (!hasAny)
        {
            throw new NotFoundException("Id Não encontrado!");
        }
        try
        {
            _context.Update(user);

        }
        catch (DbConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public async Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetUserAsync(
     int start,
     int length,
     string searchValue,
     string sortColumn,
     string sortDirection)
    {
        var query = _context.ApplicationUsers
            .Include(x => x.Maintenances)
            .AsQueryable();

        // Filtragem 
        if (!string.IsNullOrEmpty(searchValue))
        {   // Com base em outros metodos de busca, este restringe as buscas ao tipo de permissão do usuario,
            // com base no telefone, email, nome e nome de usuario.
            query = query.Where(x =>
                (x.Role != null && x.Role.ToLower().Contains(searchValue)) ||
                (x.Phone != null && x.Phone.ToLower().Contains(searchValue)) ||
                (x.Email != null && x.Email.ToLower().Contains(searchValue)) ||
                (x.Name != null && x.Name.ToLower().Contains(searchValue)) ||
                (x.UserName != null && x.UserName.ToLower().Contains(searchValue)));
        }

        // Realiza contagem após a busca
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
                Role = x.Role,
                Phone = x.Phone,
                Email = x.Email,
                Name = x.Name,
                UserName = x.UserName
            })
            .ToListAsync();

        var totalRecords = await _context.ApplicationUsers.CountAsync();

        return (data, totalRecords, filteredCount);
    }


}