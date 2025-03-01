namespace ControllRR.Domain.Entities.BrazilianTaxs;


public class IcmsOrigem
{
    public int Id { get; set; }
    public int Id_Icms_Origem { get; set; }
    public string? Codigo_Icms_Origem { get; set; }
    public string? Desc_Icms_Origem { get; set; }

    public IcmsOrigem()
    {

    }

    public IcmsOrigem(int id, int idOrigem, string? cod, string? desc)
    {
        Id = id;
        Id_Icms_Origem = idOrigem;
        Codigo_Icms_Origem = cod;
        Desc_Icms_Origem = desc;
    }
}