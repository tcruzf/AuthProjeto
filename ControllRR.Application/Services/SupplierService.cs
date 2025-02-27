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

    public async Task<SupplierDto> GetSupplierByIdAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);
        return _mapper.Map<SupplierDto>(supplier);
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
            return new OperationResultDto
            {
                Success = true,
                Id = supplier.Id
            };
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

    public async Task<List<SupplierDto>> Search(string term)
    {
        term = term?.Trim() ?? string.Empty;
        await _uow.BeginTransactionAsync();
        var supplierRepo = _uow.GetRepository<ISupplierRepository>();
        var suppliersFind = await supplierRepo.SearchAsync(term);
        return _mapper.Map<List<SupplierDto>>(suppliersFind);
    }

    public async Task<bool> CnpjExists(string cnpj)
    {
        await _uow.BeginTransactionAsync();
        var supplierRepo = _uow.GetRepository<ISupplierRepository>();
        // Remove máscara e valida formato
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
        
        if (!Supplier.ValidarCNPJ(cnpj))
            throw new ArgumentException("CNPJ inválido");

        return await supplierRepo.AnyAsync(s => s.CNPJ == cnpj);
    }

    // Isso abaixo não serve para nada ainda.
    // Será uma das novas implementações.
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