using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class AdminSeed
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        // Para que o serviço funcione corretamente, precisamos de dependencias do identity, conforme a estrutura
        // de classe de usuarios(ApplicationUser) do sistema.
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Criar roles se não existirem
        // As roles são criadas e os usuarios recebem as atribuições de 'autorizações'
        // com base em permissões.
        string[] roles = { "Admin", "Manager", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        /*
            Verificar se o admin já existe
            Verifica se o usuario admin já existe. Este usuario será o administrador do sistema
            O primeiro login deverá ser feito com essas credenciais:

            >Login(Username): t@t.com
            >Password(senha padrão):SenhaProvisoria@123
            Este usuario não terá manutenções atribuidas durante a rotina de 
            popular o banco de dados, sendo assim, pode ser excluido após a criação
            de um usuario personalizado.
              
        
        */
        var adminEmail = "t@t.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            /*  Criar novo usuário admin
                Caso não exista, o usuario será criado.
                Se por ventura exista, então nada será feito
            */    
            var user = new ApplicationUser
            {
                OperatorId = 0,
                Name = "ControllRR Systems",
                Register = 1121,
                Role = "Admin",
                Phone = null,
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true // A opção EmailConfirmed ativa imediatamente o usuario. 
                                      // Caso não esteja com valor true, então o usuario NÃO poderá realizar login.
            };

            // Criar usuário com senha
            // Executa o processo de criação do usuario passando os valores atribuidos acima.
            // Somente a senha é passada fora do objeto 'user'.
            var createResult = await userManager.CreateAsync(user, "SenhaProvisoria@123");
            
            // Condição para testar se o usuario foi corretamente criado
            // Caso retorne 'true', então aplica-se as devidas permissões ao usuario recem criado.
            if (createResult.Succeeded)
            {
                // Atribuir role de Admin
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}