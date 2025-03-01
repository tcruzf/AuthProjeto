using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class StockRepository : BaseRepository<Stock>, IStockRepository
{


    public StockRepository(ControllRRContext context) : base(context)
    {

    }

    public async Task<List<Stock>> FindAllAsync()
    {
        var stock = await _context.Stocks
        .ToListAsync();
        return stock;
    }

    public async Task<List<Stock>> SearchAsync(string term)
    {
        return await _context.Stocks
         .Where(s => s.ProductName.Contains(term) ||
                    s.ProductDescription.Contains(term) ||
                    s.ProductReference.Contains(term) ||
                    s.ProductApplication.Contains(term))
         .Include(s => s.Movements)
             .ThenInclude(m => m.Maintenance) // Carrega a manutenção relacionada
         .ToListAsync();
    }


    /// <summary>
    ///  Busca um item no estoque com base no id fornecido
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// Retorna um unico item do estoque onde o id(int) for satisfeito.
    /// </returns>
    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks
            .Include(s => s.Movements)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    /// <summary>
    /// Busca por estoque conforme um id informado com parametro.
    /// </summary>
    /// <param name="supplierId"></param>
    /// <returns>
    ///  Retorna uma lista de intens do estoque onde a consulta pelo id for satisfeita
    /// </returns>
    public async Task<List<Stock>> GetBySupplierIdAsync(int supplierId)
    {
        return await _context.Stocks
            .Where(s => s.SupplierId == supplierId)
            .ToListAsync();
    }

    public async Task UpdatePriceItem(Stock stock)
    {
        var existingProduct = await GetByIdAsync(stock.Id);
        if(existingProduct == null)
            throw new Exception("Produto Invalido!");
        
        _context.Entry(existingProduct).CurrentValues.SetValues(stock);

    }

}