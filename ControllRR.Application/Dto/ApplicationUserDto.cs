using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;

public class ApplicationUserDto
{

    public string Id { get; set; }
    //public int OperatorId { get; set; }
    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string Name { get; set; }
    [Display(Name = "Matricula")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public int Register { get; set; }
    [Display(Name = "Permissões")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public string? Role { get; set; }//

    [Display(Name = "Telefone")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? Phone { get; set; }
    public ICollection<Maintenance>? Maintenances { get; set; }
    public string Roles { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(100, ErrorMessage = "{0} deve conter entre {2} e no maximo {1} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Senha")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

/*
[Required] public string Name { get; set; }
    [Required][EmailAddress] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required][Compare("Password")] public string ConfirmPassword { get; set; }
    [Required] public int Register { get; set; }
    [Required] public string Role { get; set; }
    public string? Roles { get; set; } // Para o dropdown

    */
}
