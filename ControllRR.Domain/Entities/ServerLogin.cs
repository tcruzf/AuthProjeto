namespace ControllRR.Domain.Entities;

public class ServerLogin
{
    public int ServerId { get; set; } // Chave estrangeira para Server
    public Server Server { get; set; } // Propriedade de navegação

    public int LoginId { get; set; } // Chave estrangeira para Login
    public Login Login { get; set; } // Propriedade de navegação

    // Propriedades adicionais (opcional)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Permissions { get; set; } 
}