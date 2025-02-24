using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class SupplierService : ISupplierService
{
    private readonly IUnitOfWork _uow;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    public SupplierService(
        IUnitOfWork uow,
        ISupplierRepository supplierRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _uow = uow;
        _supplierRepository = supplierRepository;
    }

    public async Task<List<SupplierDto>> FindAllAsync()
    {
        var suppliers = await _supplierRepository.FindAllAsync();
        return _mapper.Map<List<SupplierDto>>(suppliers);
    }

    public Task<SupplierDto> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<OperationResultDto> InsertAsync(SupplierDto supplierDto)
    {
        var result = new OperationResultDto { Success = true };

        if (supplierDto == null)
            throw new Exception();
        try
        {
            await _uow.BeginTransactionAsync();
            var supplier = _mapper.Map<Supplier>(supplierDto);
            var supplierRepo = _uow.GetRepository<ISupplierRepository>();
            await supplierRepo.AddAsync(supplier);
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

    }

    private string GenerateSupplierErrorScript()
    {
        return $@"
        Swal.fire({{
            icon: 'error',
            title: 'Erro no Estoque!',
            html: `O estoque do produto <b></b> é insuficiente. <br> 
                   Quantidade solicitada: `,
            footer: '<a href='/Stocks/SearchProduct'>Verifique o estoque</a>'
        }});";
    }

}