using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class SystemRoutines : ISystemRoutines
{
    private readonly IUnitOfWork _uow;
    private readonly IDeviceService _devices;
    private readonly IMaintenanceService _maintenances;
    public  SystemRoutines(
        IUnitOfWork uow,
        IDeviceService devices,
        IMaintenanceService maintenances
                           )
    {
        _uow = uow;
        _devices = devices;
        _maintenances = maintenances;
    }
    public async Task<int> CountDevices()
    {
       return await _devices.CountDevices();
    }

    public async Task<int> CountMaintenance()
    {
        return await _maintenances.CountMaintenance();
    }
}