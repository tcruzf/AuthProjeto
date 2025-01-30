using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class MaintenanceViewModel
{
    public MaintenanceDto MaintenanceDto { get; set; }
    public ICollection<ApplicationUserDto> ApplicationUserDto { get; set; }
    public ICollection<DeviceDto>? DeviceDto { get; set; }

}