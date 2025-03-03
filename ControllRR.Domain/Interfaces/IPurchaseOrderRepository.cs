using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
{
    Task<List<PurchaseOrder>> FindAllAsync();
//    Task<List<PurchaseOrder>> SearchAsync(string term);
    Task<PurchaseOrder?> GetByIdAsync(int? id);
    Task<List<PurchaseOrder>> GetBySupplierAsync(int supplierId);
    Task CreateNewSupplierOrder(PurchaseOrder purchaseOrder);
    Task<PurchaseOrder> GetOrderByInvoiceNumber(int? invoiceNumber);
}