using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;
public class DeviceDto
{
public int Id { get; set; }
    [Display(Name = "Tipo")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public string Type { get; set; }
    [Display(Name = "Indetificador")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public string Identifier { get; set; }
    [Display(Name = "Modelo")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public string Model { get; set; }
    [Display(Name = "Nº Serie")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    public string SerialNumber { get; set; }
    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!!!")]
    [StringLength(300, MinimumLength = 0, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres")]
    public string? DeviceDescription { get; set; }

    public int SectorId { get; set; }
    [Display(Name = "Setor")]
    public Sector? Sector { get; set; }
    public ICollection<Maintenance>? Maintenances { get; set; }
}