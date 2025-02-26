using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface ISupplierService
{

    Task<List<SupplierDto>> FindAllAsync();
    Task<SupplierDto> GetSupplierByIdAsync(int id);
    Task<OperationResultDto> InsertAsync (SupplierDto supplierDto);

}