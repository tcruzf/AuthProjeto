using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Cria uma nova ordem de fornecimento com base nos dados fornecidos.
    /// </summary>
    /// <param name="purchaseOrderDto">Dados da ordem de fornecimento.</param>
    /// <returns>Resultado da operação.</returns>
    public async Task<OperationResultDto> CreateNewSupplierOrder(PurchaseOrderDto purchaseOrderDto)
    {
        if (purchaseOrderDto == null)
            throw new ArgumentNullException(nameof(purchaseOrderDto));

        try
        {
            await _uow.BeginTransactionAsync();

            // Mapeia DTO para entidade e salva a ordem para obter o ID.
            var order = _mapper.Map<PurchaseOrder>(purchaseOrderDto);
            var purchaseOrderRepo = _uow.GetRepository<IPurchaseOrderRepository>();
            var purchaseItemRepo = _uow.GetRepository<IPurchaseItemRepository>();
            var stockRepo = _uow.GetRepository<IStockRepository>();

            // TODO: Verificar quais campos são necessários para a NFe (nota fiscal) e adicionar suporte a isso.
            // Atualmente, CFOPId é fixo porque ainda não é recuperado na tela de cadastro.

            //order.TotalAmount = purchaseOrderDto.Items.Sum(item => item.Quantity * item.UnitPrice);
            //order.TotalTaxes = order.TotalAmount * 0.18m;
            //order.NFeSource = NFeSource.Nacional;

            await purchaseOrderRepo.AddAsync(order);
            await _uow.SaveChangesAsync(); // Gera o ID da ordem

            // Associa os itens à ordem e os salva.
            foreach (var itemDto in purchaseOrderDto.Items)
            {
                var item = _mapper.Map<PurchaseItem>(itemDto);
                item.PurchaseOrderId = order.Id;
                //item.PurchaseOrder.CFOPId = order.CFOPId;
                await purchaseItemRepo.AddAsync(item);

                // FIXME: Ao atualizar o preço de compra no estoque, todos os produtos recebem o mesmo valor.
                // Isso precisa ser corrigido antes de reativar esta parte do código.
                /* 
                var stockItem = await stockRepo.GetByIdAsync(itemDto.StockId);
                if(stockItem != null)
                {
                    stockItem.PurchasePrice = itemDto.UnitPrice;
                    await stockRepo.UpdateAsync(stockItem);
                }
                */
            }

            await _uow.SaveChangesAsync();
            await _uow.CommitAsync();

            return new OperationResultDto { Success = true };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message); // TODO: Melhorar tratamento de erro (evitar capturar e relançar sem contexto adicional).
        }
    }

    public async Task<List<PurchaseOrderDto>> Search(string term)
    {
        var stockRepo = _uow.GetRepository<IPurchaseOrderRepository>();
        var stocksFind = await stockRepo.SearchAsync(
            term,
            additionalFilter: null, //  filtro adicional 
            includes: q => q
                .Include(s => s.Supplier)
                    ,
           // x => x.Id.ToString(),
            x => x.IssuerCNPJ,
            x => x.IssuerIE,
            x => x.NFeAccessKey,
            x => x.InvoiceNumber
        );

        return _mapper.Map<List<PurchaseOrderDto>>(stocksFind);
    }
}