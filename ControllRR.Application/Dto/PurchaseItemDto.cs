using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


public class PurchaseItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}