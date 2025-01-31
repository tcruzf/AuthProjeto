using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;

public class StockManagementDto
{
    public int Id { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Fechamento")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime MovementDate { get; set; }
    [Display(Name = "Descrição Simples")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string MovementType { get; set; }
    [Display(Name = "Descrição Simples")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public int Quantity { get; set; }
    //  Não incluir StockId ou StockDto aqui porque se não da merda. BL\Z?kkk
    public string FormattedMovementDate => MovementDate.ToString("dd/MM/yyyy");
}