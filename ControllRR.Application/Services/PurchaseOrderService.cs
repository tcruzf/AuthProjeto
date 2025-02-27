using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    public PurchaseOrderService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }
    public async Task<List<PurchaseOrderDto>> GetBySupplierAsync(int supplierId)
    {
        await _uow.BeginTransactionAsync();
        //
        var purchaseOrderRepo = _uow.GetRepository<IPurchaseOrderRepository>();
        var orders = await purchaseOrderRepo.GetBySupplierAsync(supplierId);
       return orders?.Select(o => _mapper.Map<PurchaseOrderDto>(o)).ToList() 
           ?? new List<PurchaseOrderDto>();
    }

    public Task<List<PurchaseOrder>> GetOrdersById(int id)
    {
        throw new NotImplementedException();
    }

    Task<List<PurchaseOrderDto>> IPurchaseOrderService.GetOrdersById(int id)
    {
        throw new NotImplementedException();
    }
}