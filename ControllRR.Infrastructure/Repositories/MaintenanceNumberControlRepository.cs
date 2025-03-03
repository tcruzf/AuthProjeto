using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;


public class MaintenanceNumberControlRepository : GenericRepository<MaintenanceNumberControl>, IMaintenanceNumberControlRepository
{
    public MaintenanceNumberControlRepository(ControllRRContext context) : base(context) { }


    /// <summary>
    /// Checa o valor do controle de manutenções diretamente no banco de dados
    /// </summary>
    /// <returns>Um numero de manutenção pre determinado para inicio de controle</returns>
    public async Task<MaintenanceNumberControl> GetCurrentControlAsync()
    {
        var control = await _context.MaintenanceNumberControls.FirstOrDefaultAsync();
        if (control == null)
        {
            control = new MaintenanceNumberControl { CurrentNumber = 99 };
            await _context.AddAsync(control);
        }
        return control;
    }
}