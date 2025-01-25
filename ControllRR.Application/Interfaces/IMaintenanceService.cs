using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;
public interface IMaintenanceService
{
    Task<List<MaintenanceDto>> FindAllAsync();
    Task<MaintenanceDto> FindByIdAsync(int id);
    Task InsertAsync (MaintenanceDto maintenanceDto);
    Task RemoveAsync(int id);
    Task UpdateAsync(MaintenanceDto maintenance);
    Task FinalizeAsync(int id);
      Task<object> GetMaintenanceDataTableAsync( int start,
    int length,
    string searchValue,
    string sortColumn,
    string sortDirection);
}