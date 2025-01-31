using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;

public class StockManagementDto
{
    public int Id { get; set; }
    public DateTime MovementDate { get; set; }
    public string MovementType { get; set; }
    public int Quantity { get; set; }
    //  Não incluir StockId ou StockDto aqui porque se não da merda. BL\Z?kkk
    public string FormattedMovementDate => MovementDate.ToString("dd/MM/yyyy");
}