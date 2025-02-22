using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Interfaces;
public interface ISectorService
{
    Task<List<SectorDto>> FindAllAsync();
    Task<SectorDto> FindByIdAsync(int id);

    Task InsertAsync(SectorDto sectorDto);
    Task<object> GetSectorAsync(
    int start,
    int length,
    string searchValue,
    string sortColumn,
    string sortDirection);

    Task UpdateAsync(SectorDto sectorDto);
}