namespace ControllRR.Domain.Entities;

public class Server
{
    public int Id { get; set; }
    public string ServerName { get; set; }
    public string ServerIP { get; set; }
    public string ServerPassword { get; set; }

    // Propriedade de navegação para a relação muitos-para-muitos
    public ICollection<ServerLogin> ServerLogins { get; set; } = new List<ServerLogin>();
}