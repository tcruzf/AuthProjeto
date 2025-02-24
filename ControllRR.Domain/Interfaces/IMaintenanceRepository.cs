using ControllRR.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using ControllRR.Domain.PaginatedResult;
namespace ControllRR.Domain.Interfaces;
public interface IMaintenanceRepository : IRepository<Maintenance>
{
  Task<List<Maintenance>> FindAllAsync();
 Task<Maintenance?> GetByIdWithDetailsAsync(
      int id,
      bool includeProducts = true,
      bool includeDevice = true,
      bool includeUser = true);
 
  Task FinalizeMaintenanceAsync(int id);
   Task<PaginatedResult<object>> GetPaginatedMaintenancesAsync(int start, int length, string searchValue, string sortColumn, string sortDirection);
  
  Task<bool> ExistsAsync(int id);
  Task<int> CountMaintenance();
  Task RemoveAsync(int id);
  Task<Dictionary<string, int>> GetMaintenanceCountByMonth();
}