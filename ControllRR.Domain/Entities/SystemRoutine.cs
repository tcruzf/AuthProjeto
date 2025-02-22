namespace ControllRR.Domain.Entities;



public class SystemRoutine
{

    public int Id { get; set; }
    public int? Version { get; set; }
    public string? AutoUpdate { get; set; }
    public string? Url { get; set; }
    public string? Protocol { get; set; }
    public int? Dashboard { get; set; } = null;
    public int? Scripts { get; set; }
    public string? PathProject { get; set; }


    public SystemRoutine()
    {

    }

    public SystemRoutine(int id, int? version, string? autoUpdate, string? url, string? protocol, int? dashboard, int? scripts, string? pathProject)
    {
        Id = id;
        Version = version;
        AutoUpdate = autoUpdate;
        Url = url;
        Protocol = protocol;
        Dashboard = dashboard;
        Scripts = scripts;
        PathProject = pathProject;

    }


}