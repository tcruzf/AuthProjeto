using ControllRR.Domain.Entities;
namespace ControllRR.Domain.Interfaces;


public interface IMaintenanceNumberControlRepository : IRepository<MaintenanceNumberControl>
{
    Task<MaintenanceNumberControl> GetCurrentControlAsync();
}