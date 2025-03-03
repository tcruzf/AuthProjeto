using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;

public class SupplierDto
{
    public int Id { get; set; }
    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? Name { get; set; }
    [Display(Name = "CNPJ")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    //Aumentei o tamanho maximo, acontecia erro com formatação do cnpj
    [StringLength(18, MinimumLength = 14, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? CNPJ { get; set; }
    [Display(Name = "Email")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? ContactEmail { get; set; }
    [Display(Name = "Telefone")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Endereço")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? Address { get; set; }
    [Display(Name = "CEP")]
     public string? CEP { get; set; }
     [Display(Name = "Códio de regime tributário")]
    public string? CRT { get; set; }
    [Display(Name = "Inscrição Estadual")]
    public string? IssuerIE { get; set; }
    [Display(Name = "Tipo de pessoa")]
    public PersonType? PersonType { get; set; }
    [Display(Name = "Área/Ramo de atuação")]
    public string? ActivityArea { get; set; }


}