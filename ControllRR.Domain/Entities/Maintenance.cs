using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class Maintenance
{
    public int Id { get; set; }
    public string SimpleDesc { get; set; }
    public string Description { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime CloseDate { get; set; }
    public MaintenanceStatus Status { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public Device? Device { get; set; }
    public int DeviceId { get; set; }

    public int MaintenanceNumber { get; set; }


    public Maintenance()
    {

    }
    public Maintenance(int id, string description, string simpleDesc, DateTime openDate, DateTime closeDate, MaintenanceStatus status, User user, Device device, int maintenanceNumber)
    //public Maintenance(int id, string description, string simpleDesc, DateTime openDate, DateTime closeDate, MaintenanceStatus status, int maintenanceNumber)
    {
        Id = id;
        Description = description;
        SimpleDesc = simpleDesc;
        OpenDate = openDate;
        CloseDate = closeDate;
        Status = status;
        User = user;
        Device = device;
        MaintenanceNumber = maintenanceNumber;
    }


}
