using ControllRR.Application.Dto;
using ControllRR.Application.Exceptions;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Interfaces;

namespace ControllRR.Application.Services;

public class SystemRoutines : ISystemRoutines
{
    private readonly IUnitOfWork _uow;
    private readonly IDeviceService _devices;
    private readonly IMaintenanceService _maintenances;
    public SystemRoutines(
        IUnitOfWork uow,
        IDeviceService devices,
        IMaintenanceService maintenances
                           )
    {
        _uow = uow;
        _devices = devices;
        _maintenances = maintenances;
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

   public async Task<(double CpuUsage, double MemoryUsage)> GetServerStatus()
    {
        if (IsLinux())
        {
            return await GetLinuxMetricsAsync();
        }
        else
        {
            return await GetWindowsMetrics();
        }
    }

    private async Task<(double CpuUsage, double MemoryUsage)> GetLinuxMetricsAsync()
    {
        try
        {
            // Primeira leitura da CPU
            var (prevTotal, prevIdle) = await ReadCpuStats();
            await Task.Delay(1000); // Espera 1 segundo
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

    private async Task<(double total, double idle)> ReadCpuStats()
    {
        var cpuLine = (await File.ReadAllLinesAsync("/proc/stat"))[0];
        var values = cpuLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                          .Skip(1)
                          .Select(double.Parse)
                          .ToArray();
        return (values.Sum(), values[3]);
    }

    private async Task<(double used, double total)> ReadMemoryStats()
    {
        var memLines = await File.ReadAllLinesAsync("/proc/meminfo");
        var total = ParseMemValue(memLines[0]);
        var free = ParseMemValue(memLines[1]);
        var buffers = ParseMemValue(memLines[2]);
        var cached = ParseMemValue(memLines[3]);

        var used = total - (free + buffers + cached);
        return (used, total);
    }

    private double ParseMemValue(string line)
    {
        return double.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
    }

    private async Task<(double CpuUsage, double MemoryUsage)> GetWindowsMetrics()
    {
        throw new NotImplementedException();
    }

    public async Task<Dictionary<string, int>> MaintenanceMonth()
   {
        return await _maintenances.MaintenanceMonth();
   }
}