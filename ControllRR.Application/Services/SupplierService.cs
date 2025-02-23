using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class SupplierService : ISupplierService
{
    private readonly IUnitOfWork _uow;
    private readonly ISupplierRepository _repository;
    private readonly IMapper _mapper;
    public SupplierService(
        IUnitOfWork uow,
        ISupplierRepository repository,
        IMapper mapper)
    {
        _mapper = mapper;
        _uow = uow;
        _repository = repository;
    }

    public async Task<List<SupplierDto>> FindAllAsync()
    {
        var suppliers = await _repository.FindAllAsync();
        return _mapper.Map<List<SupplierDto>>(suppliers);
    }

    public Task<SupplierDto> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SupplierDto> InsertAsync(MaintenanceDto maintenanceDto)
    {
        throw new NotImplementedException();
    }
}