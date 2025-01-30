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
    public int Register { get; set; }

    public string? Role { get; set; }

    [Display(Name = "Telefone")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? Phone { get; set; }
    public ICollection<Maintenance>? Maintenances { get; set; }
    public string Roles { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }


}
