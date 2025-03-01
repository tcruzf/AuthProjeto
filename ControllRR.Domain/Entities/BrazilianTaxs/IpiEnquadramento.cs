namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IpiEnquadramento
{
    public int Id { get; set; }
    public int Id_Ipi_Enquadramento { get; set; }
    public string? Desc_Ipi_Enquadramento { get; set; }
    public string? Codigo_Ipi_Enquadramento { get; set; }
    public int Id_Ipi_Operacao { get; set; }
    public IpiOperacao? IpiOperacao {get; set;}


    public IpiEnquadramento()
    {

    }

    public IpiEnquadramento(int id, int idEnquadramento, string? nome)
    {
        Id = id;
        Id_Ipi_Enquadramento = idEnquadramento;
        Desc_Ipi_Enquadramento = nome;
    }
}