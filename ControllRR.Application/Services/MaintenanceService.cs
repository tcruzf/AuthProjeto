
using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IMapper _mapper;
    private readonly IStockRepository _stockRepository;

    public MaintenanceService(IMaintenanceRepository maintenanceRepository, IMapper mapper, IStockRepository stockRepository)
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
        _stockRepository = stockRepository;
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
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

        foreach (var product in maintenance.MaintenanceProducts)
        {
            var stock = await _stockRepository.GetByIdAsync(product.StockId);

            if (stock.ProductQuantity < product.QuantityUsed)
            {
                throw new Exception($"Estoque insuficiente: {stock.ProductName}");
            }
            stock.ProductQuantity -= product.QuantityUsed;
            await _stockRepository.UpdateAsync(stock);
        }

        // Se ao menos uma coisa não der errado, então talvez dê pra persistir os dados
        await _maintenanceRepository.InsertAsync(maintenance);

    }

    public async Task UpdateAsync(MaintenanceDto maintenanceDto)
    {
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);

        foreach (var product in maintenance.MaintenanceProducts)
        {
            var stock = await _stockRepository.GetByIdAsync(product.StockId);

            if (stock.ProductQuantity < product.QuantityUsed)
            {
                throw new Exception($"Estoque insuficiente: {stock.ProductName}");
            }
            System.Console.WriteLine("Remove one product!");
            System.Console.WriteLine(stock.ProductQuantity);
            System.Console.WriteLine("After remove:");

            stock.ProductQuantity -= product.QuantityUsed;
            System.Console.WriteLine(stock.ProductQuantity);
            await _stockRepository.UpdateAsync(stock);
        }

        await _maintenanceRepository.UpdateAsync(maintenance);
        //await _maintenanceRepository.SaveChangesAsync();
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