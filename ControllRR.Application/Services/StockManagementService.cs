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
    private readonly IUnitOfWork _uow;
    public StockManagementService(IStockManagementRepository stockManagementRepository,
     IMapper mapper, IStockRepository stockRepository, IMaintenanceRepository maintenanceRepository,
     IUnitOfWork uow
     )
    {
        _stockManagementRepository = stockManagementRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
        _maintenanceRepository = maintenanceRepository;
        _uow = uow;
    }

    public async Task<List<StockManagementDto>> FindAllAsync()
    {
        var stockProductInfo = await _stockManagementRepository.FindAllAsync();
        return _mapper.Map<List<StockManagementDto>>(stockProductInfo);
    }

    public async Task AddMovementAsync(int stockId, StockMovementType type, int quantity, DateTime movementDate, int? maintenanceId = null)
    {
        //await _uow.BeginTransactionAsync();
        var stock = await _stockRepository.GetByIdAsync(stockId);
        if (stock == null)
            throw new Exception($"Estoque ID {stockId} não encontrado.");

        if (!maintenanceId.HasValue)
        {
            if (type == StockMovementType.Entrada)
                stock.ProductQuantity += quantity;
            else
                stock.ProductQuantity -= quantity;

            await _stockRepository.UpdateAsync(stock);
            //await _uow.SaveChangesAsync();
        }

        var movement = new StockManagement
        {
            StockId = stockId,
            MovementType = type,
            Quantity = quantity,
            MovementDate = movementDate,
            MaintenanceId = maintenanceId
        };

        await _stockManagementRepository.AddAsync(movement);
        await _uow.SaveChangesAsync();
        //await _uow.CommitAsync();
       
    }



    public async Task<int> GetCurrentStockAsync(int stockId)
    {
        var stock = await _stockRepository.GetByIdAsync(stockId);
        return stock.ProductQuantity;
    }


}