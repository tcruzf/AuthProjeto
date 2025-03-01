namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class ICMS
{
    public int Id { get; set; }
    public int? Id_Icms { get; set; }
    public int? Codigo_Icms { get; set; }
    public string? Desc_Icms { get; set; }

    public ICMS()
    {

    }
    
    public ICMS(int id, int? idcms, int? cod, string? desc)
    {
        Id = id;
        Id_Icms = idcms;
        Codigo_Icms = cod;
        Desc_Icms = desc;
    }
}