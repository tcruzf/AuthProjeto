using ControllRR.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace ControllRR.Domain.Interfaces;
public interface IMaintenanceRepository
{
  Task<List<Maintenance>> FindAllAsync();
 Task<Maintenance?> FindByIdAsync(
      int id,
      bool includeProducts = true,
      bool includeDevice = true,
      bool includeUser = true);
  Task InsertAsync(Maintenance maintenance);
  Task RemoveAsync(int id);
  Task UpdateAsync(Maintenance maintenance);
  Task FinalizeAsync(int id);
  Task<(IEnumerable<object> Data, int TotalRecords, int FilteredRecords)> GetMaintenancesAsync(
                int start,
                int length,
                string searchValue,
                string sortColumn,
                string sortDirection);
  
  Task<bool> ExistsAsync(int id);
}