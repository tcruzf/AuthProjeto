using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ControllRR.Domain.Entities;

public class ApplicationUser : IdentityUser

{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Register { get; set; }
    public string? Role { get; set; }

    public ApplicationUser()
    {

    }
}