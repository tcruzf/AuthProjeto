
using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;


public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _maintenanceRepository;
    private readonly IMapper _mapper;

    public MaintenanceService(IMaintenanceRepository maintenanceRepository, IMapper mapper)
    {
        _maintenanceRepository = maintenanceRepository;
        _mapper = mapper;
    }

    public async Task<List<MaintenanceDto>> FindAllAsync()
    {

        var maintenance = await _maintenanceRepository.FindAllAsync();
        return _mapper.Map<List<MaintenanceDto>>(maintenance);

    }

    public async Task<MaintenanceDto> FindByIdAsync(int id)
    {
        var maintenance = await _maintenanceRepository.FindByIdAsync(id);
        return _mapper.Map<MaintenanceDto>(maintenance);

    }

    public async Task InsertAsync(MaintenanceDto maintenanceDto)
    {
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);
        
       await _maintenanceRepository.InsertAsync(maintenance);
        //await _maintenanceRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        await _maintenanceRepository.RemoveAsync(id);

    }

    public async Task UpdateAsync(MaintenanceDto maintenanceDto)
    {
        var maintenance = _mapper.Map<Maintenance>(maintenanceDto);
       await _maintenanceRepository.UpdateAsync(maintenance);
       //await _maintenanceRepository.SaveChangesAsync();
    }

    public async Task FinalizeAsync(int id)
    {
      await  _maintenanceRepository.FinalizeAsync(id);

    }
    //
    public async Task<object> GetMaintenanceDataTableAsync(
           int start,
           int length,
           string searchValue,
           string sortColumn,
           string sortDirection)
    {
        (IEnumerable<object> data, int totalRecords, int filteredRecords) =
            await _maintenanceRepository.GetMaintenancesAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(), // Pode ajustar conforme necessário
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }
}