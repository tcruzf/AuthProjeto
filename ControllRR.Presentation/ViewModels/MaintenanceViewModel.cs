using System.ComponentModel.DataAnnotations;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class MaintenanceViewModel
{
    public MaintenanceDto MaintenanceDto { get; set; } = new MaintenanceDto();
    [Display(Name = "Usuario")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public ICollection<ApplicationUserDto> ApplicationUserDto { get; set; } = new List<ApplicationUserDto>();
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public ICollection<DeviceDto>? DeviceDto { get; set; } = new List<DeviceDto>();
    public List<StockDto> AvailableStocks { get; set; } = new List<StockDto>();
    public List<MaintenanceProductDto> SelectedProducts { get; set; } = new List<MaintenanceProductDto>();


}