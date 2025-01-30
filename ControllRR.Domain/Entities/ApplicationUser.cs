using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Domain.Entities;

public class ApplicationUser : IdentityUser

{
    public int OperatorId { get; set; }
    public string Name { get; set; }
    public int Register { get; set; }
    public string? Role { get; set; }

    public string? Phone { get; set; }

     public ICollection<Maintenance>? Maintenances { get; set; } = new List<Maintenance>();
    public ApplicationUser()
    {

    }

    public ApplicationUser(int operatorId, string name, string? phone, int register, string? role)
    {
        OperatorId = operatorId;
        Name = name;
        Role = role;
        Phone = phone;
    }

}