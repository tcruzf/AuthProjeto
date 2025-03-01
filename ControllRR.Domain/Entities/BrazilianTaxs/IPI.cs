namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IPI
{
   public int Id { get; set; }
    public int Id_Ipi { get; set; }
    public string? Cod_Ipi { get; set; }
    public string? Desc_Ipi { get; set; }

    public IPI()
    {

    }

    public IPI(int id,int idIpi, string? cod, string? desc)
    {
        Id = id;
        Id_Ipi = idIpi;
        Cod_Ipi = cod;
        Desc_Ipi = desc;
    }
}