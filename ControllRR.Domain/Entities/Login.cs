namespace ControllRR.Domain.Entities;
public class Login
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    // Propriedade de navegação para a relação muitos-para-muitos
    public ICollection<ServerLogin> ServerLogins { get; set; } = new List<ServerLogin>();
}