using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class TaxConfiguration
{
    public int Id { get; set; }
    public TaxType TaxType { get; set; }
    public decimal Rate { get; set; }
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }

    public TaxConfiguration()
    {

    }

    public TaxConfiguration(int id, TaxType taxType, decimal rate, Stock? stock)
    {
        Id = id;
        TaxType = taxType;
        Rate = rate;
        Stock = stock;
        StockId = stock?.Id;

    }
}