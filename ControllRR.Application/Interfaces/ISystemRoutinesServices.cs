using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;


public interface ISystemRoutines
{
    Task<int> CountDevices();
    Task<int> CountMaintenance();
}