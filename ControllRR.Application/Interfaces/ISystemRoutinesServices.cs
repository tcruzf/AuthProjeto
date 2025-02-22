using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;


public interface ISystemRoutines
{
    Task<int> CountDevices();
    Task<int> CountMaintenance();
    Task<(double CpuUsage, double MemoryUsage)> GetServerStatus();
    Task<Dictionary<string, int>> MaintenanceMonth();
    Task<SystemRoutinesDto> FindConfigAsync(int id);
    Task CreateConfigAsync(SystemRoutinesDto systemRoutinesDto);
    Task UpdateConfigAsync(SystemRoutinesDto systemRoutinesDto);
}