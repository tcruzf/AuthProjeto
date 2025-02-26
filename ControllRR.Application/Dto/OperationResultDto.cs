public class OperationResultDto
{
    public int? Id { get; set; } //Um id para que eu possa conseguir manipular as informações do fornecedor ao realizar redirecionamento. Ta certo isso?!
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? AlertScript { get; set; } // Script para exibir no frontend
}