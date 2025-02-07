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
    private readonly IMaintenanceRepository _maintenanceRepository;
    public StockManagementService(IStockManagementRepository stockManagementRepository, IMapper mapper, IStockRepository stockRepository, IMaintenanceRepository maintenanceRepository)
    {
        _stockManagementRepository = stockManagementRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
        _maintenanceRepository = maintenanceRepository;
    }

    public async Task<List<StockManagementDto>> FindAllAsync()
    {
        var stockProductInfo = await _stockManagementRepository.FindAllAsync();
        return _mapper.Map<List<StockManagementDto>>(stockProductInfo);
    }

    public async Task AddMovementAsync(int stockId, StockMovementType type, int quantity, DateTime movementDate, int? maintenanceId = null)
    {
        if (!maintenanceId.HasValue)
        {
            var stock = await _stockRepository.GetByIdAsync(stockId);

            if (type == StockMovementType.Entrada) // Caso o tipo de movimentaçaõ do produto for entrada, então
                stock.ProductQuantity += quantity; // adiciona-se o valor de entrada ao valor atual(estoque)
            else
                stock.ProductQuantity -= quantity; // Caso seja uma movimentação de saida, decresce o valor movimentado.

            await _stockRepository.UpdateAsync(stock);
        }
        // Remove as linhas que alteram o estoque
        var movement = new StockManagement
        {
            StockId = stockId,
            MovementType = type,
            Quantity = quantity,
            MovementDate = movementDate,
            MaintenanceId = maintenanceId
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