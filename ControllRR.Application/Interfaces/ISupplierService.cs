using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface ISupplierService
{

    Task<List<SupplierDto>> FindAllAsync();
    Task<SupplierDto> FindByIdAsync(int id);
    Task<SupplierDto> InsertAsync (MaintenanceDto maintenanceDto);
}