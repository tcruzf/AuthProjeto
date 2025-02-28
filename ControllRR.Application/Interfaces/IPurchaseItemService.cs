using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;


public interface IPurchaseItemService
{
    Task<PurchaseItemDto> InsertNewItem(PurchaseItemDto purchaseItemDto);
}