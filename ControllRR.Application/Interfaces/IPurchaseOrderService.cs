using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;



public interface IPurchaseOrderService
{
    Task<List<PurchaseOrderDto>> GetOrdersById(int id);
    Task<List<PurchaseOrderDto>> GetBySupplierAsync(int supplierId);
}