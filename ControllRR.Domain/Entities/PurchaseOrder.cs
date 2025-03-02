using ControllRR.Domain.Entities.BrazilianTaxs;
using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;


public class PurchaseOrder
{
    public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TotalTaxes { get; set; }
    public ICollection<PurchaseItem?> Items { get; set; } = new List<PurchaseItem>();
    public ICollection<FinancialRecord?> FinancialRecords { get; set; } = new List<FinancialRecord>();
    public string? NFeAccessKey { get; set; }
    public DateTime? NFeEmissionDate { get; set; }
    public int StockId { get; set; }
    //Novos
    public NFeSource? NFeSource { get; set; }
    public NFeStatus? NFeStatus { get; set; }//
    public string? IssuerCNPJ { get; set; }   // CNPJ da empresa que emite a nota
    public string? IssuerIE { get; set; }     // Inscrição Estadual
    public FreightMode? FreightMode { get; set; } // Não sei se frete em inglês se escreve assim. Estou confuso kkk
   
    public PaymentMethod? PaymentMethod { get; set; }
    // Chave de Acesso da NFe referenciada em caso de ser devolução ou nota complementar
    public string? ReferenceNFeKey { get; set; }
    public NFeOperationType? OperationType { get; set; }




    public PurchaseOrder()
    {

    }

    public PurchaseOrder(DateTime orderDate, Supplier supplier)
    {
        OrderDate = orderDate;
        Supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
        SupplierId = supplier.Id;
        Items = new List<PurchaseItem>();
    }
}