using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IMapper _mapper;
    public SectorService(ISectorRepository sectorRepository, IMapper mapper)
    {
        _sectorRepository = sectorRepository;
        _mapper = mapper;
    }

    public async Task<List<Sector>> FindAllAsync()
    {
       return await _sectorRepository.FindAllAsync();
    }

    public async Task InsertAsync(SectorDto sectorDto)
    {
        var sector = _mapper.Map<Sector>(sectorDto);
        await _sectorRepository.InsertAsync(sector);
    }

    public async Task<Sector> FindByIdAsync(int id)
    {
        return await _sectorRepository.FindByIdAsync(id);
    }

     public async Task<object> GetSectorAsync(int start, int length, string searchValue, string sortColumn, string sortDirection)
    {
       (IEnumerable<object> data, int totalRecords, int filteredRecords) =
             await _sectorRepository.GetSectorAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(), // Pode ajustar conforme necessário
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }

    

  
}