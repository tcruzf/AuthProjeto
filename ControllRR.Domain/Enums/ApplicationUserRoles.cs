namespace ControllRR.Domain.Enums;

public enum ApplicationUserRoles
{
    Admin,
    Member,

    Manager
 
}

public static class ApplicationUserRolesExtensions
{
    public static string GetGuid(this ApplicationUserRoles role)
    {
        return role switch
        {
            ApplicationUserRoles.Admin => "asadfasd",
            ApplicationUserRoles.Manager => "Manager",
            ApplicationUserRoles.Member => "Member",
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };
    }
}