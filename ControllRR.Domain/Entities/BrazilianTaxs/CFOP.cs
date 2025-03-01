namespace ControllRR.Domain.Entities.BrazilianTaxs;


public class CFOP
{

    public int Id { get; set; }
    public double? Codigo_Cfop { get; set; }
    public string? Desc_Cfop { get; set; }

    public CFOP()
    {

    }

    public CFOP(double? cod, string? desc)
    {
        Codigo_Cfop = cod;
        Desc_Cfop = desc;
    }
}