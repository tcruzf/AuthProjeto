
using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;
    private readonly IStockManagementService _stockManagementService;

    public MaintenanceService(IMaintenanceRepository maintenanceRepository, IMapper mapper, IStockRepository stockRepository, IStockManagementService stockManagementService)
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
        _stockManagementService = stockManagementService;
    }

    public async Task<List<MaintenanceDto>> FindAllAsync()
    {

        var maintenance = await _maintenanceRepository.FindAllAsync();
        return _mapper.Map<List<MaintenanceDto>>(maintenance);

    }

    public async Task<MaintenanceDto> FindByIdAsync(int id)
    {
        var maintenance = await _maintenanceRepository.FindByIdAsync(id);
        return _mapper.Map<MaintenanceDto>(maintenance);

    }

    public async Task InsertAsync(MaintenanceDto maintenanceDto)
    {
        await using var transaction = await _maintenanceRepository.BeginTransactionAsync();

        try
        {
            var maintenance = _mapper.Map<Maintenance>(maintenanceDto);
            await _maintenanceRepository.InsertAsync(maintenance);
            //await _maintenanceRepository.SaveChangesAsync();

            //var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

            foreach (var product in maintenance.MaintenanceProducts)
            {
                var stock = await _stockRepository.GetByIdAsync(product.StockId);

                if (stock.ProductQuantity < product.QuantityUsed)
                    throw new Exception($"Estoque insuficiente: {stock.ProductName}");

                stock.ProductQuantity -= product.QuantityUsed; // Única alteração do estoque
                await _stockRepository.UpdateAsync(stock);

                await _stockManagementService.AddMovementAsync( // Agora só registra a movimentação
                    product.StockId,
                    StockMovementType.Saida,
                    product.QuantityUsed,
                    DateTime.Now,
                    maintenance.Id
                );
            }
            await _maintenanceRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        // Se ao menos uma coisa não der errado, então talvez dê pra persistir os dados
        //await _maintenanceRepository.InsertAsync(maintenance);

    }

    public async Task UpdateAsync(MaintenanceDto maintenanceDto)
    {
        await using var transaction = await _maintenanceRepository.BeginTransactionAsync();

        try
        {
            var existingMaintenance = await _maintenanceRepository.FindByIdAsync(maintenanceDto.Id, includeProducts: true);
            var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

            foreach (var existingProduct in existingMaintenance.MaintenanceProducts)
            {
                var updatedProduct = maintenance.MaintenanceProducts
                    .FirstOrDefault(p => p.StockId == existingProduct.StockId);

                if (updatedProduct == null)
                {
                    await RestockProduct(existingProduct, maintenanceDto.Id);
                }
                else
                {
                    await UpdateStockQuantity(existingProduct, updatedProduct, maintenanceDto.Id);
                }
            }

            var newProducts = maintenance.MaintenanceProducts
                .Where(p => !existingMaintenance.MaintenanceProducts.Any(ep => ep.StockId == p.StockId));

            foreach (var newProduct in newProducts)
            {
                await DeductStock(newProduct, maintenanceDto.Id);
            }

            await _maintenanceRepository.UpdateAsync(maintenance);
            await _maintenanceRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateStockQuantity(MaintenanceProduct original, MaintenanceProduct updated, int maintenanceId)
    {
        var quantityDifference = updated.QuantityUsed - original.QuantityUsed;

        if (quantityDifference != 0)
        {
            var stock = await _stockRepository.GetByIdAsync(original.StockId);
            stock.ProductQuantity -= quantityDifference; // Única atualização

            await _stockRepository.UpdateAsync(stock);

            await _stockManagementService.AddMovementAsync(
                original.StockId,
                quantityDifference > 0 ? StockMovementType.Saida : StockMovementType.Entrada,
                Math.Abs(quantityDifference),
                DateTime.Now,
                maintenanceId
            );
        }
    }

    private async Task DeductStock(MaintenanceProduct product, int maintenanceId)
    {
        var stock = await _stockRepository.GetByIdAsync(product.StockId);
        stock.ProductQuantity -= product.QuantityUsed;
        await _stockRepository.UpdateAsync(stock);

        await _stockManagementService.AddMovementAsync(
            product.StockId,
            StockMovementType.Saida,
            product.QuantityUsed,
            DateTime.Now,
            maintenanceId
        );
    }

    private async Task RestockProduct(MaintenanceProduct product, int maintenanceId)
    {
        var stock = await _stockRepository.GetByIdAsync(product.StockId);
        stock.ProductQuantity += product.QuantityUsed;
        await _stockRepository.UpdateAsync(stock);

        await _stockManagementService.AddMovementAsync(
            product.StockId,
            StockMovementType.Entrada,
            product.QuantityUsed,
            DateTime.Now,
            maintenanceId
        );
    }


    public async Task FinalizeAsync(int id)
    {
        await _maintenanceRepository.FinalizeAsync(id);

    }

    public async Task RemoveAsync(int id)
    {
        await _maintenanceRepository.RemoveAsync(id);

    }
    //
    public async Task<object> GetMaintenanceDataTableAsync(
           int start,
           int length,
           string searchValue,
           string sortColumn,
           string sortDirection)
    {
        (IEnumerable<object> data, int totalRecords, int filteredRecords) =
            await _maintenanceRepository.GetMaintenancesAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(),
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }
}