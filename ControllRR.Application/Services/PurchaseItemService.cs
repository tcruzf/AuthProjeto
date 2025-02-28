using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class PurchaseItemService : IPurchaseItemService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public PurchaseItemService(IUnitOfWork uow, IMapper mapper)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task<PurchaseItemDto> InsertNewItem(PurchaseItemDto purchaseItemDto)
    {
        var purchaseItemRepo = _uow.GetRepository<IPurchaseItemRepository>();
        var itens = _mapper.Map<PurchaseItem>(purchaseItemDto);
        await purchaseItemRepo.AddAsync(itens);
        await _uow.SaveChangesAsync();
        return _mapper.Map<PurchaseItemDto>(itens);
    }
}