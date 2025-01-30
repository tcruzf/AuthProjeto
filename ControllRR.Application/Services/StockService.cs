using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Interfaces;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;
    public StockService(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
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
}
