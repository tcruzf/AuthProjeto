namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IcmsModalidadeBC
{
    public int Id { get; set; }
    public int? Id_Icms_Modalidade_bc { get; set; }
    public int? Codigo_Icms_Modalidade_bc { get; set; }
    public string? Desc_Icms_Modalidade_bc { get; set; }

    public IcmsModalidadeBC()
    {}

    public IcmsModalidadeBC(int id, int? idModalidade, int? cod, string? desc)
    {
        Id = id;
        Id_Icms_Modalidade_bc = idModalidade;
        Codigo_Icms_Modalidade_bc = cod;
        Desc_Icms_Modalidade_bc = desc;
    }

}