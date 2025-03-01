namespace ControllRR.Domain.Entities.BrazilianTaxs;


public class CNAE
{
    public int Id { get; set; }
    public string? Codigo_Cnae { get; set; }
    public string? Desc_Cnae { get; set; }


    public CNAE()
    {

    }

    public CNAE(string? cod, string? desc)
    {
        Codigo_Cnae = cod;
        Desc_Cnae = desc;
    }
}