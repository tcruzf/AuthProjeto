using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class MaintenanceViewModel
{
    public MaintenanceDto MaintenanceDto { get; set; }
    public ICollection<UserDto> UserDto { get; set; }
    public ICollection<DeviceDto>? DeviceDto { get; set; }

}