using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class AdminSeed
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Criar roles se não existirem
        string[] roles = { "Admin", "Manager", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Verificar se o admin já existe
        var adminEmail = "t@t.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            // Criar novo usuário admin
            var user = new ApplicationUser
            {
                OperatorId = 0,
                Name = "ControllRR Systems",
                Register = 1121,
                Role = "Admin",
                Phone = null,
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true // Importante para login imediato
            };

            // Criar usuário com senha
            var createResult = await userManager.CreateAsync(user, "SenhaProvisoria@123");
            
            if (createResult.Succeeded)
            {
                // Atribuir role de Admin
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}