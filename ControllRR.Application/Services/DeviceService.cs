using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
namespace ControllRR.Application.Services;

using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Domain.Interfaces;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMapper _mapper;

    public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
    }
    public async Task<List<DeviceDto>> FindAllAsync()
    {
       var devices =  await _deviceRepository.FindAllAsync();
       return _mapper.Map<List<DeviceDto>>(devices);
    }

    public async Task<DeviceDto> FindByIdAsync(int id)
    {
        var devices = await _deviceRepository.FindByIdAsync(id);
        return _mapper.Map<DeviceDto>(devices);
    }

    public async Task<DeviceDto> GetMaintenancesAsync(int id)
    {
        var device = await  _deviceRepository.GetMaintenancesAsync(id);
        return _mapper.Map<DeviceDto>(device);
    }
 
    public async Task InsertAsync(DeviceDto deviceDto)
    {
        var device = _mapper.Map<Device>(deviceDto);
        await _deviceRepository.InsertAsync(device);
       
    }

  public async Task UpdateAsync(DeviceDto deviceDto)
  {
    var device = _mapper.Map<Device>(deviceDto);
    await _deviceRepository.UpdateAsync(device);
  }

   

    public async Task<object> GetDeviceAsync(int start, int length, string searchValue, string sortColumn, string sortDirection)
    {
       (IEnumerable<object> data, int totalRecords, int filteredRecords) =
             await _deviceRepository.GetDevicesAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(), // Pode ajustar conforme necessário
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }
}