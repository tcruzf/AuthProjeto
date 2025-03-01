namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IcmsDesoneracao
{
    public int Id { get; set; }
    public int? Id_Icms_Desoneracao { get; set; }
    public int? Codigo_Icms_Desoneracao { get; set; }
    public string? Desc_Icms_Desoneracao { get; set; }

    public IcmsDesoneracao()
    {}

    public IcmsDesoneracao(int id, int? idDesoneracao, int? cod, string? desc)
    {
        Id = id;
        Id_Icms_Desoneracao = idDesoneracao;
        Codigo_Icms_Desoneracao = cod;
        Desc_Icms_Desoneracao = desc;
    }

}