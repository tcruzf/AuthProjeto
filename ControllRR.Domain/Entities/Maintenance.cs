using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class Maintenance
{
    public int Id { get; set; }
    public string? SimpleDesc { get; set; }
    public string? Description { get; set; }
    public DateTime? OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public MaintenanceStatus Status { get; set; }
    // Entidade Maintenance
    public string? ApplicationUserId { get; set; } // ✅ Tipo string para compatibilidade com IdentityUser
    public ApplicationUser? ApplicationUser { get; set; } // ✅ Nome simplificado

    public Device? Device { get; set; }
    public int? DeviceId { get; set; }

    public int? MaintenanceNumber { get; set; }


    public Maintenance()
    {

    }
    public Maintenance(int id, string description, string simpleDesc, DateTime openDate,
      DateTime closeDate, MaintenanceStatus status, ApplicationUser user, Device device, int maintenanceNumber)
    {
        Id = id;
        Description = description;
        SimpleDesc = simpleDesc;
        OpenDate = openDate;
        CloseDate = closeDate;
        Status = status;
        ApplicationUser = user;
        ApplicationUserId = user?.Id; // ✅ Definir FK
        Device = device;
        DeviceId = device?.Id ?? 0; // ✅
        MaintenanceNumber = maintenanceNumber;
    }


}
