namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class CSOSN
{
    public int Id { get; set; }
    public int? Codigo_Csosn { get; set; }
    public string? Name_Csosn { get; set; }
    public string? Desc_Csosn { get; set; }

    public CSOSN()
    {

    }

    public CSOSN(int id, int? codigo, string? name, string? desc)
    {
        Id = id;
        Codigo_Csosn = codigo;
        Name_Csosn = name;
        Desc_Csosn = desc;

    }

}