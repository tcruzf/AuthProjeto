namespace ControllRR.Application.Dto;

public class SystemRoutinesDto
{
    public int Id { get; set; }
    public int? Version { get; set; }
    public string? AutoUpdate { get; set; }
    public string? Url { get; set; }
    public string? Protocol { get; set; }
    public int? Dashboard { get; set; } = null;
    public int? Scripts { get; set; }
    public string? PathProject { get; set; }
}