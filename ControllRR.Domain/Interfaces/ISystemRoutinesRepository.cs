
using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface ISystemRoutinesRepository
{

    Task<SystemRoutine> FindConfigAsync(int id);
    Task CreateConfigAsync(SystemRoutine systemRoutine);
    Task UpdateConfigAsync(SystemRoutine systemRoutine);
}