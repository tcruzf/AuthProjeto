namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class PIS
{
    public int Id { get; set; }
    public int Id_Pis { get; set; }
    public int Cod_Pis { get; set; }
    public string? Desc_Pis { get; set; }

    public PIS()
    {

    }

    public PIS(int id, int idPis, int cod, string? desc)
    {
        Id = id;
        Id_Pis = idPis;
        Cod_Pis = cod;
        Desc_Pis = desc;
    }
}