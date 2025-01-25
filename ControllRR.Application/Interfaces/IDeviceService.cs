using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;

public interface IDeviceService
{
     Task<List<DeviceDto>> FindAllAsync();
     Task<DeviceDto> FindByIdAsync(int id);
     Task InsertAsync(DeviceDto device);
     Task UpdateAsync(DeviceDto device);

     Task<DeviceDto> GetMaintenancesAsync(int id);
      Task<object> GetDeviceAsync( int start,
    int length,
    string searchValue,
    string sortColumn,
    string sortDirection);

}