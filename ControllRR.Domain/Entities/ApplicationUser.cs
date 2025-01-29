using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Domain.Entities;

public class ApplicationUser : IdentityUser

{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Register { get; set; }
    public string? Role { get; set; }

    public string? Phone { get; set; }

    ICollection<Maintenance>? Maintenances{ get; set; } = new List<Maintenance>();

    public ApplicationUser()
    {

    }

    public ApplicationUser(int id, string name, int register, string? role, string? phone)
    {
        Id = id;
        Name = name;
        Role = role;
        Phone = phone;
        //Maintenances = new List<Maintenance>();
    }

}