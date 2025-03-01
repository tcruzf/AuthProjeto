namespace ControllRR.Domain.Entities.BrazilianTaxs;


public class NFeSource
{
    public int Id { get; set; }
    public string? NFeTypeOperation { get; set; }

    public NFeSource()
    {

    }

    public NFeSource(int id, string? nfeTypeOperation)
    {
        Id = id;
        NFeTypeOperation = nfeTypeOperation;
    }
}