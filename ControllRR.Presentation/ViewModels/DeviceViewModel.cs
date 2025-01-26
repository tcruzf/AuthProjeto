using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class DeviceViewModel
{
    public DeviceDto DeviceDto { get; set; }
    public ICollection<MaintenanceDto>? MaintenanceDtos { get; set; }
    public ICollection<SectorDto> Sector { get; set; }
}