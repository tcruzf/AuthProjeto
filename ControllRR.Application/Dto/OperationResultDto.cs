public class OperationResultDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? AlertScript { get; set; } // Script para exibir no frontend
}