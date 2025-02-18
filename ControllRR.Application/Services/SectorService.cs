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
    private readonly IUnitOfWork _uow;
    public SectorService(
        ISectorRepository sectorRepository,
        IMapper mapper,
        IUnitOfWork uow
        )
    {
        _sectorRepository = sectorRepository;
        _mapper = mapper;
        _uow = uow;
    }

    public async Task<List<SectorDto>> FindAllAsync()
    {
        var sectors = await _sectorRepository.FindAllAsync();
        return _mapper.Map<List<SectorDto>>(sectors);

    }

    public async Task InsertAsync(SectorDto sectorDto)
    {
        await _uow.BeginTransactionAsync();
        var sector = _mapper.Map<Sector>(sectorDto);
        await _sectorRepository.InsertAsync(sector);
        await _uow.SaveChangesAsync();
        await _uow.CommitAsync();


    }

    public async Task<SectorDto> FindByIdAsync(int id)
    {
        var sector = await _sectorRepository.FindByIdAsync(id);
        return _mapper.Map<SectorDto>(sector);

    }

    public async Task<object> GetSectorAsync(int start, int length, string searchValue, string sortColumn, string sortDirection)
    {
        (IEnumerable<object> data, int totalRecords, int filteredRecords) =
              await _sectorRepository.GetSectorAsync(start, length, searchValue, sortColumn, sortDirection);

        return new
        {
            draw = Guid.NewGuid().ToString(),
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data
        };
    }

    public async Task UpdateAsync(SectorDto sectorDto)
    {
        await _uow.BeginTransactionAsync();
        var sector = _mapper.Map<Sector>(sectorDto);
        await _sectorRepository.UpdateAsync(sector);
        await _uow.SaveChangesAsync();
        await _uow.CommitAsync();

    }



}