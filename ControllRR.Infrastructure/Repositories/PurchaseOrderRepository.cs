using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
{
    public PurchaseOrderRepository(ControllRRContext context) : base(context)
    {

    }

    public async Task<List<PurchaseOrder>> FindAllAsync()
    {
        return await _context.PurchaseOrders.ToListAsync();
    }

    public async Task<PurchaseOrder?> GetByIdAsync(int? id)
    {
        if (id == null)
            throw new Exception("O id fornecido não é valido!");
        return await _context.PurchaseOrders.Include(po => po.Supplier)
                             .Include(po => po.Items)
                                .ThenInclude(item => item.Stock)
                             .Include(po => po.FinancialRecords)
                             .FirstOrDefaultAsync(po => po.Id == id);


    }

    public async Task<List<PurchaseOrder>> GetBySupplierAsync(int supplierId)
    {
       
         return await _context.PurchaseOrders
        .AsNoTracking() // Evita rastreamento de entidades
        .Include(po => po.Items)
        .Where(po => po.SupplierId == supplierId)
        .ToListAsync() ?? new List<PurchaseOrder>();
    }

  
    public async Task CreateNewSupplierOrder(PurchaseOrder purchaseOrder)
    {
        await AddAsync(purchaseOrder);
       
    }
}