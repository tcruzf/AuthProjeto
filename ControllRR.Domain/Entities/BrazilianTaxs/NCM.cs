namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class NCM
{
    public int Id { get; set; }
    public int Id_Ncm { get; set; }
    public string? Cod_Ncm { get; set; }
    public string? Nome_Ncm { get; set; }


    public NCM()
    {

    }


    public NCM(int id, int IdNcm, string? cod, string? nome)
    {
        Id = id;
        Id_Ncm = IdNcm;
        Cod_Ncm = cod;
        Nome_Ncm = nome;
    }
}