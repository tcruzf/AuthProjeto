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
    private readonly IPurchaseItemService _purchaseItemService;
    private readonly IStockService _stockService;

    public PurchaseOrderService(IMapper mapper, IUnitOfWork uow, IPurchaseItemService purchaseItemService, IStockService stockService)
    {
        _mapper = mapper;
        _uow = uow;
        _purchaseItemService = purchaseItemService;
        _stockService = stockService;
    }
    public async Task<List<PurchaseOrderDto>> GetBySupplierAsync(int supplierId)
    {
        //await _uow.BeginTransactionAsync();
        //
        var purchaseOrderRepo = _uow.GetRepository<IPurchaseOrderRepository>();
        var orders = await purchaseOrderRepo.GetBySupplierAsync(supplierId);
        return orders?.Select(o => _mapper.Map<PurchaseOrderDto>(o)).ToList()
            ?? new List<PurchaseOrderDto>();
    }

    Task<List<PurchaseOrderDto>> IPurchaseOrderService.GetOrdersById(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<OperationResultDto> CreateNewSupplierOrder(PurchaseOrderDto purchaseOrderDto)
    {
        if (purchaseOrderDto == null)
            throw new ArgumentNullException(nameof(purchaseOrderDto));

        try
        {
            await _uow.BeginTransactionAsync();

            // Mapeia DTO para entidade 
            var order = _mapper.Map<PurchaseOrder>(purchaseOrderDto);
 
            // Salva a ordem primeiro para obter o ID
            var purchaseOrderRepo = _uow.GetRepository<IPurchaseOrderRepository>();
            var purchaseItemRepo = _uow.GetRepository<IPurchaseItemRepository>();
            var stockRepo = _uow.GetRepository<IStockRepository>();
            //Gostaria de atualizar o preço do produto com o valor que vem em purchaseOrderDto
            order.TotalAmount =  purchaseOrderDto.Items.Sum(item => item.Quantity * item.UnitPrice);
            order.TotalTaxes = purchaseOrderDto.TotalTaxes = purchaseOrderDto.TotalAmount * 0.18m;
            order.CFOPId = "1.126";
            await purchaseOrderRepo.AddAsync(order);
            await _uow.SaveChangesAsync(); // Gera o ID da ordem

            // Atribui o ID da ordem aos itens e salva
            foreach (var itemDto in purchaseOrderDto.Items)
            {
                var item = _mapper.Map<PurchaseItem>(itemDto);
                item.PurchaseOrderId = order.Id; // Define a chave estrangeira
                
                await purchaseItemRepo.AddAsync(item);

                /*var stockItem = await stockRepo.GetByIdAsync(itemDto.StockId);
                if(stockItem != null)
                {
                    stockItem.PurchasePrice = itemDto.UnitPrice;
                    await stockRepo.UpdateAsync(stockItem);
                }*/
            }
           

            await _uow.SaveChangesAsync();
            await _uow.CommitAsync();

            return new OperationResultDto { Success = true };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}