using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class StockManagementService : IStockManagementService
{

    private readonly IStockManagementRepository _stockManagementRepository;
    private readonly IStockRepository _stockRepository;
     private readonly IMapper _mapper;
    public StockManagementService(IStockManagementRepository stockManagementRepository, IMapper mapper, IStockRepository stockRepository)
    {
        _stockManagementRepository = stockManagementRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    public async Task<List<StockManagementDto>> FindAllAsync()
    {
        var stockProductInfo = await _stockManagementRepository.FindAllAsync();
        return _mapper.Map<List<StockManagementDto>>(stockProductInfo);
    }

     public async Task AddMovementAsync(int stockId, StockMovementType type, int quantity, DateTime movementDate)
    {
         Console.WriteLine($"Tipo recebido: {type} ({(int)type})"); // Log para depuração
      
        var stock = await _stockRepository.GetByIdAsync(stockId); // 


        if (type == StockMovementType.Entrada)
            stock.ProductQuantity += quantity;
        else
            stock.ProductQuantity -= quantity;

        
        if (stock.ProductQuantity < 0)
            throw new InvalidOperationException("Estoque não pode ser negativo!");

       
        var movement = new StockManagement
        {
            StockId = stockId,
            MovementType = type,
            Quantity = quantity,
            MovementDate = movementDate
        };

        await _stockManagementRepository.AddAsync(movement);
        await _stockManagementRepository.SaveChangesAsync();
    }

    public async Task<int> GetCurrentStockAsync(int stockId)
    {
        var stock = await _stockRepository.GetByIdAsync(stockId);
        return stock.ProductQuantity;
    }

    
}