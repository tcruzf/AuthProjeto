
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
    private readonly IUnitOfWork _uow;

    public MaintenanceService(
     IMaintenanceRepository maintenanceRepository,
     IMapper mapper,
     IStockRepository stockRepository,
     IStockManagementService stockManagementService,
     IUnitOfWork uow
     )
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
        _stockManagementService = stockManagementService;
        _uow = uow;
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

    public async Task<OperationResultDto> InsertAsync(MaintenanceDto maintenanceDto)
    {
        //await using var transaction = await _uow.BeginTransactionAsync();
        var result = new OperationResultDto { Success = true };
        try
        {
            await _uow.BeginTransactionAsync();
            var maintenance = _mapper.Map<Maintenance>(maintenanceDto);
            await _maintenanceRepository.InsertAsync(maintenance);
            await _uow.SaveChangesAsync();

            foreach (var product in maintenance.MaintenanceProducts)
            {
                var stock = await _stockRepository.GetByIdAsync(product.StockId);

                if (stock.ProductQuantity < product.QuantityUsed)
                {

                    await _uow.RollbackAsync();
                    result.Success = false;
                    result.AlertScript = GenerateStockErrorScript(stock.ProductName, product.QuantityUsed);
                    //await transaction.RollbackAsync();
                    return result;

                    //throw new Exception($"Estoque insuficiente: {stock.ProductName}");
                }
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
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return new OperationResultDto { Success = true };
        }
        catch (Exception ex)
        {
            await _uow.RollbackAsync();
            return new OperationResultDto
            {
                Success = false,
                Message = ex.Message
            };

        }
        // Se ao menos uma coisa não der errado, então talvez dê pra persistir os dados
        //await _maintenanceRepository.InsertAsync(maintenance);
        // Só para me atualizar, não é necessario salvar mais nada aqui, tudo que preciso está dentro do bloco try/catch.
        // Foi bom ter sofrido por uma semana. Serviu de aprendizado.

    }

    private string GenerateStockErrorScript(string productName, int quantity)
    {
        return $@"
        Swal.fire({{
            icon: 'error',
            title: 'Erro no Estoque!',
            html: `O estoque do produto <b>{productName}</b> é insuficiente. <br> 
                   Quantidade solicitada: ${quantity}`,
            footer: '<a href='/Stocks/SearchProduct'>Verifique o estoque</a>'
        }});";
    }
    public async Task<OperationResultDto> UpdateAsync(MaintenanceDto maintenanceDto)
    {
        //await using var context = _contextFactory.CreateDbContext();
        // await using var transaction = await _maintenanceRepository.BeginTransactionAsync();

        try
        {
            await _uow.BeginTransactionAsync();
            var existingMaintenance = await _maintenanceRepository.FindByIdAsync(maintenanceDto.Id, includeProducts: true);
            System.Console.WriteLine("######################");
            System.Console.WriteLine(existingMaintenance.MaintenanceProducts.Count());
            var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

            foreach (var existingProduct in existingMaintenance.MaintenanceProducts)
            {
                var updatedProduct = maintenance.MaintenanceProducts
                    .FirstOrDefault(p => p.StockId == existingProduct.StockId);

                if (updatedProduct == null)
                {
                    await RestockProduct(existingProduct, maintenanceDto.Id);
                    await _uow.SaveChangesAsync();
                }
                else
                {
                    await UpdateStockQuantity(existingProduct, updatedProduct, maintenanceDto.Id);
                    await _uow.SaveChangesAsync();
                }
            }

            var newProducts = maintenance.MaintenanceProducts
                .Where(p => !existingMaintenance.MaintenanceProducts.Any(ep => ep.StockId == p.StockId));

            foreach (var newProduct in newProducts)
            {
                await DeductStock(newProduct, maintenanceDto.Id);
                await _uow.SaveChangesAsync();
            }

            await _maintenanceRepository.UpdateAsync(maintenance);
            await _uow.SaveChangesAsync();
            await _uow.CommitAsync();
            return new OperationResultDto { Success = true };

        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateStockQuantity(MaintenanceProduct original, MaintenanceProduct updated, int maintenanceId)
    {
        //await _uow.BeginTransactionAsync();
        if (updated.QuantityUsed < 0)
            throw new Exception("Quantidade não pode ser negativa!");

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
        await _uow.SaveChangesAsync();
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

    // Retorna o numero de manutenção registradas no bano de dados
    public async Task<int> CountMaintenance()
    {
        return await _maintenanceRepository.CountMaintenance();
    }

    public async Task<Dictionary<string, int>> MaintenanceMonth()
   {
        return await _maintenanceRepository.MaintenanceMonth();
   }

}