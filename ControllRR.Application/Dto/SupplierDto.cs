using System.ComponentModel.DataAnnotations;

namespace ControllRR.Application.Dto;

public class SupplierDto
{
    public int Id { get; set; }
    [Display(Name = "Nome")]
    public string? Name { get; set; }
    [Display(Name = "CNPJ")]
    public string? CNPJ { get; set; }
    [Display(Name = "Email")]
    public string? ContactEmail { get; set; }
    [Display(Name = "Telefone")]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Endereço")]
    public string? Address { get; set; }

}