using AutoMapper;
using ControllRR.Application.Dto;
using ControllRR.Application.Exceptions;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class SystemRoutines : ISystemRoutines
{
    private readonly IUnitOfWork _uow;
    private readonly IDeviceService _devices;
    private readonly IMaintenanceService _maintenances;
    private readonly IMapper _mapper;
    private readonly ISystemRoutinesRepository _systemRoutinesRepository;
    public SystemRoutines(
        IUnitOfWork uow,
        IDeviceService devices,
        IMaintenanceService maintenances,
        IMapper mapper,
        ISystemRoutinesRepository systemRoutinesRepository
                           )
    {
        _uow = uow;
        _devices = devices;
        _maintenances = maintenances;
        _mapper = mapper;
        _systemRoutinesRepository = systemRoutinesRepository;
    }
    public async Task<int> CountDevices()
    {
        return await _devices.CountDevices();
    }

    public async Task<int> CountMaintenance()
    {
        return await _maintenances.CountMaintenance();
    }

    private bool IsLinux()
    {
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
            System.Runtime.InteropServices.OSPlatform.Linux);
    }

    /// <summary>
    /// Testa pelo sistema operacional, caso seja linux, chama o metodo reponsavel por colher as informações referentes
    /// a hardware disponiveis 
    /// </summary>
    /// <returns></returns>
    public async Task<(double CpuUsage, double MemoryUsage)> GetServerStatus()
    {
        if (IsLinux())
        {
            return await GetLinuxMetricsAsync();
        }
        else
        {
            // Não implementada por não ter um sistema operacional windows disponivel
            return await GetWindowsMetrics();
        }
    }
    //

    /// <summary>
    /// Pega as informações referentes ao hardware(memoria e cpu) para montar o json com os valores que serão exibidos
    /// no dashboard
    /// </summary>
    /// <returns></returns>
    private async Task<(double CpuUsage, double MemoryUsage)> GetLinuxMetricsAsync()
    {
        try
        {
            // Primeira leitura da CPU
            var (prevTotal, prevIdle) = await ReadCpuStats();
            await Task.Delay(3000); // Espera 3 segundos
            // Segunda leitura da CPU
            var (currTotal, currIdle) = await ReadCpuStats();

            // Cálculo do uso da CPU
            var totalDiff = currTotal - prevTotal;
            var idleDiff = currIdle - prevIdle;
            var cpuUsage = (totalDiff - idleDiff) / totalDiff * 100;

            // Leitura da memória
            var (usedMem, totalMem) = await ReadMemoryStats();
            var memoryUsage = (usedMem / totalMem) * 100;

            return (Math.Round(cpuUsage, 1), Math.Round(memoryUsage, 1));
        }
        catch (Exception ex)
        {
            // Logar erro adequadamente
            return (0, 0);
        }
    }

    /// <summary>
    /// Faz a leitura dos dados relacionados a cpu do servidor que hospeda a aplicação
    /// </summary>
    /// <returns></returns>
    private async Task<(double total, double idle)> ReadCpuStats()
    {
        var cpuLine = (await File.ReadAllLinesAsync("/proc/stat"))[0];
        var values = cpuLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Skip(1)
                          .Select(double.Parse)
                          .ToArray();
        return (values.Sum(), values[3]);
    }

    /// <summary>
    /// Faz a leitura dos dados relacionados a memoria do servidor que hospeda a aplicação
    /// </summary>
    /// <returns></returns>
    private async Task<(double used, double total)> ReadMemoryStats()
    {
        var memLines = await File.ReadAllLinesAsync("/proc/meminfo");

        // Busca os valores pelos nomes corretos
        double total = GetMemValue(memLines, "MemTotal:");
        double free = GetMemValue(memLines, "MemFree:");
        double buffers = GetMemValue(memLines, "Buffers:");
        double cached = GetMemValue(memLines, "Cached:");
        double sReclaimable = GetMemValue(memLines, "SReclaimable:"); // Para sistemas mais novos

        // Cálculo ajustado considerando buffers e cached (incluindo SReclaimable)
        var used = total - (free + buffers + cached + sReclaimable);
        return (used, total);
    }

    private double GetMemValue(string[] lines, string prefix)
    {
        var line = lines.FirstOrDefault(l => l.StartsWith(prefix));
        if (line == null) return 0;

        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return double.Parse(parts[1]);
    }

    private double ParseMemValue(string line)
    {
        return double.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
    }

    // Não implementada por não ter o sistema operacional windows disponivel.
    // Depois eu testo isso com um windows, por enquanto é irrelevante
    // 
    private async Task<(double CpuUsage, double MemoryUsage)> GetWindowsMetrics()
    {
        throw new NotImplementedException();
    }

    public async Task<Dictionary<string, int>> MaintenanceMonth()
    {
        return await _maintenances.MaintenanceMonth();
    }

    public async Task<SystemRoutinesDto> FindConfigAsync(int id)
    {
        await _uow.BeginTransactionAsync();
        var result = await _systemRoutinesRepository.FindConfigAsync(id);
        return _mapper.Map<SystemRoutinesDto>(result);

    }

    public async Task CreateConfigAsync(SystemRoutinesDto systemRoutinesDto)
    {
        await _uow.BeginTransactionAsync();
        var config = _mapper.Map<SystemRoutine>(systemRoutinesDto);
        await _systemRoutinesRepository.CreateConfigAsync(config);
        await _uow.SaveChangesAsync();
        await _uow.CommitAsync();

    }

    public Task UpdateConfigAsync(SystemRoutinesDto systemRoutinesDto)
    {
        throw new NotImplementedException();
    }
}