using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Interfaces;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    private readonly IStockManagementService _stockManagementService;
    private readonly IUnitOfWork _uow;
    public StockService(IStockRepository stockRepository, IMapper mapper, IStockManagementService stockManagementService, IUnitOfWork uow)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
        _stockManagementService = stockManagementService;
        _uow = uow;
    }

    public async Task<List<StockDto>> FindAllAsync()
    {
        var stockProduct = await _stockRepository.FindAllAsync();
        return _mapper.Map<List<StockDto>>(stockProduct);
    }

    public async Task<List<StockDto>> Search(string term)
    {
        var stocks = await _stockRepository.SearchAsync(term);
        return _mapper.Map<List<StockDto>>(stocks);
    }

    //
    public async Task<StockDto> CreateProductWithInitialMovementAsync(StockDto stockDto)
    {
        if(stockDto == null)
            throw new Exception();
       
        try
        {

            await _uow.BeginTransactionAsync();
            // Cria e persiste o Stock
            var stock = _mapper.Map<Stock>(stockDto);
            stock.ProductQuantity = 0;
            var stockRepo = _uow.GetRepository<IStockRepository>();
            await stockRepo.AddAsync(stock);
            await _uow.SaveChangesAsync(); 
            // Adiciona a movimentação inicial
            await _stockManagementService.AddMovementAsync(
                stock.Id,
                StockMovementType.Entrada,
                stockDto.ProductQuantity,
                DateTime.Now,
                null
            );

            await _uow.SaveChangesAsync(); // Persiste a movimentação
            await _uow.CommitAsync();

            return _mapper.Map<StockDto>(stock);
        }
        catch(Exception ex)
        {
            throw new Exception("Stock Service Exception", ex);
            throw;
        }
    }

    public async Task<StockDto> GetProductWithMovementsAsync(int id)
    {
        var product = await _stockRepository.GetByIdAsync(id);

        return _mapper.Map<StockDto>(product);
    }
}