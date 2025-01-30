using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class StockViewModel
{
    //public MaintenanceDto MaintenanceDto { get; set; } = new MaintenanceDto();
    public ICollection<StockManagement> StockManagementDto { get; set; } = new List<StockManagement>();
    public StockDto? StockDto { get; set; }

}