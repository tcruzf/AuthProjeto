namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IcmsModalidadeST
{
    public int Id { get; set; }
    public int? Id_Icms_Modalidade_st { get; set; }
    public int? Codigo_Icms_Modalidade_st { get; set; }
    public string? Desc_Icms_Modalidade_st { get; set; }

    public IcmsModalidadeST()
    { }

    public IcmsModalidadeST(int? idModalidade, int? cod, string? desc)
    {
        Id = Id;
        Id_Icms_Modalidade_st = idModalidade;
        Codigo_Icms_Modalidade_st = cod;
        Desc_Icms_Modalidade_st = desc;
    }

}