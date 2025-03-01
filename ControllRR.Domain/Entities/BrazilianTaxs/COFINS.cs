namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class COFINS
{

    public int Id { get; set; }
    public int Codigo_Cofins { get; set; }
    public string? Desc_Cofins { get; set; }

    public COFINS()
    {

    }

    public COFINS(int codigo, string? desc)
    {
        Codigo_Cofins = codigo;
        Desc_Cofins = desc;
    }

}