using System.ComponentModel.DataAnnotations;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class DashboardViewModel
{
    public int DeviceCount { get; set; }
    public int MaintenanceCount { get; set; }


}