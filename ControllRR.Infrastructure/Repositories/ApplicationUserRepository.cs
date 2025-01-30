/*
    Classe ApplicationUserRepository ainda em desenvolvimento
    Função: Responsavel por lidar com a inserção, remoção e atualização dos usuarios do sistema.
    Todos os metodos lidam somente com usuarios que estão autorizados a logar no sistema.
*/
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ControllRRContext _controllRRContext;

    public ApplicationUserRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    // Retorna todos os usuarios cadastrados do sistema
    public async Task<List<ApplicationUser>> FindAllAsync()
    {
        //Teste usando sql raw apenas para fins didaticos
        // Retorna uma lista de usuarios que tem autorização para se logar no sistema
        var query = @"SELECT *
                  FROM `AspNetRoles`
                  "; 
        var execution = await _controllRRContext.ApplicationUsers
            .FromSqlRaw(query)
            .AsNoTracking()
            .ToListAsync();
        return execution;
      
    }

    //Busca usuario especifico com base no id fornecido
    public async Task<ApplicationUser> FindByIdAsync(int id)
    {   
       
        return await _controllRRContext.ApplicationUsers
        .FirstOrDefaultAsync(x => x.OperatorId == id);

    }
    // Adiciona novo usuario ao sistema
    public async Task InsertAsync(ApplicationUser applicationUser)
    {
        _controllRRContext.AddAsync(applicationUser);
        await _controllRRContext.SaveChangesAsync();

    }

    // Remove usuario dos registros
    public async Task RemoveAsync(int id)
    {
        var obj = await _controllRRContext.ApplicationUsers
        .FirstOrDefaultAsync(u => u.OperatorId == id);

        _controllRRContext.Remove(obj);
        await _controllRRContext.SaveChangesAsync();

    }
    // Persiste as informações no banco de dados
    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }

}