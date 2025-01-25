using System.ComponentModel.DataAnnotations;
using ControllRR.Application.Dto;

namespace ControllRR.Application.Dto;

public class UserDto
{
    public int Id { get; set; }
    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string Name { get; set; }
    [Display(Name = "Telefone")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string Phone { get; set; }
    [Display(Name = "Matricula")]
    public double Register { get; set; }
    public ICollection<MaintenanceDto>? Maintenances { get; set; }
}