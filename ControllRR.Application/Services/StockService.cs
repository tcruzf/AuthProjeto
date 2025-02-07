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
    public StockService(IStockRepository stockRepository, IMapper mapper, IStockManagementService stockManagementService)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
        _stockManagementService = stockManagementService;
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

    public async Task<StockDto> CreateProductWithInitialMovementAsync(StockDto stockDto)
    {
        // Mapeia o DTO para Stock com quantidade 0
        var stock = _mapper.Map<Stock>(stockDto);
        stock.ProductQuantity = 0; // Tenho que inicializar com 0

        await _stockRepository.InsertAsync(stock);

        // Adiciona movimentação inicial (Entrada)
        await _stockManagementService.AddMovementAsync(
            stock.Id,
        StockMovementType.Entrada,
        stockDto.ProductQuantity,
        DateTime.Now,
        null // Aqui deve ser esse valor?
        );

        return _mapper.Map<StockDto>(stock);
    }


    public async Task AddAsync(StockDto stock)
    {
        var stockNew = _mapper.Map<Stock>(stock);
        await _stockRepository.InsertAsync(stockNew);

    }


}
