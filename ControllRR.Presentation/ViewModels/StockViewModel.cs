using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Presentation.ViewModels;

public class StockViewModel
{
    public StockDto? StockDto { get; set; }
    public List<StockManagementDto> Movements { get; set; } = new(); // Já está mapeado no DTO!

   
}