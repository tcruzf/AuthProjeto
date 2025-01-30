using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class MaintenanceViewModel
{
    public MaintenanceDto MaintenanceDto { get; set; } = new MaintenanceDto();
    public ICollection<ApplicationUserDto> ApplicationUserDto { get; set; } = new List<ApplicationUserDto>();
    public ICollection<DeviceDto>? DeviceDto { get; set; } = new List<DeviceDto>();

}