using Microsoft.AspNetCore.Identity;

namespace ControllRR.Domain.Entities;

public class ApplicationUser : IdentityUser

{
    public int Register { get; set; }
    public ApplicationUser()
    {

    }
}