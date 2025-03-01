    namespace ControllRR.Domain.Entities.BrazilianTaxs;

public class IpiOperacao
{
    public int Id { get; set; }
    public int Id_Ipi_Operacao { get; set; }
    public string? Nome_Ipi_Operacao { get; set; }

    public IpiOperacao()
    {

    }

    public IpiOperacao(int id ,int idOperacao, string? nome)
    {
        Id = id;
        Id_Ipi_Operacao = idOperacao;
        Nome_Ipi_Operacao = nome;
    }
}