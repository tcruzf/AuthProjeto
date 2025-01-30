using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class StockManagementService : IStockManagementService
{

    private readonly IStockManagementRepository _stockManagementRepository;
     private readonly IMapper _mapper;
    public StockManagementService(IStockManagementRepository stockManagementRepository, IMapper mapper)
    {
        _stockManagementRepository = stockManagementRepository;
        _mapper = mapper;
    }

    public async Task<List<StockManagementDto>> FindAllAsync()
    {
        var stockProductInfo = await _stockManagementRepository.FindAllAsync();
        return _mapper.Map<List<StockManagementDto>>(stockProductInfo);
    }

    
}