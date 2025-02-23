using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;


public class FinancialRecord
{
     public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public FinancialRecordType Type { get; set; } // Receita/Despesa
    public int? PurchaseOrderId { get; set; }
    public int? SaleId { get; set; }
    public decimal TotalTaxes {get; set;}

    public FinancialRecord()
    {

    }

    public FinancialRecord(DateTime date, decimal amount, FinancialRecordType type, decimal totalTaxes)
    {
        Date = date;
        Amount = amount;
        Type = type;
        TotalTaxes = totalTaxes;
    }
}