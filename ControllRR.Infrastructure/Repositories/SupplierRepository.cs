using System.Linq.Expressions;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ControllRRContext context) : base(context)
    {

    }

    // Retorna uma lista de fornecedores
    public async Task<List<Supplier>> FindAllAsync()
    {
        return await _context.Suppliers.ToListAsync();
    }

    // Busca um unico fornecedor com base no id fornecido.
    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await _context.Suppliers.FindAsync(id);


    }

    // Logo irei implementar essa busca.
    // O objetivo é que seja bem similar a busca por produtos https://one.tva.one:8443/Stocks/SearchProduct
    // O form estará vazio, terei uma busca tem tempo real, após satisfeita a busca com a escolha inserida, carrego todos os outros campos. xD
    public async Task<List<Supplier>> SearchAsync(string term)
    {
        return await _context.Suppliers.Where(x => x.Name.Contains(term) ||
             x.FantasyName.Contains(term) ||
             x.CNPJ.Contains(term)
             ).ToListAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<Supplier, bool>> predicate)
    {
        return await _context.Suppliers.AnyAsync(predicate);
    }

}