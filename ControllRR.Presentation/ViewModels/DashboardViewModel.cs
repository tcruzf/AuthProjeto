using System.ComponentModel.DataAnnotations;
using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;

namespace ControllRR.Presentation.ViewModels;


public class DashboardViewModel
{
    public int DeviceCount { get; set; }
    public int MaintenanceCount { get; set; }
    public double CpuUsage { get; set; }
    public double MemoryUsage { get; set; }
    public double TotalMemory { get; set; }
     public Dictionary<string, int> MaintenancesByMonth { get; set; }

}