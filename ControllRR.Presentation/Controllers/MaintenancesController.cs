using ControllRR.Application.Interfaces;
using ControllRR.Infrastructure.Repositories;
using ControllRR.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Presentation.ViewModels;
using System.Diagnostics;
using ControllRR.Domain.Entities;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Application.Dto;

namespace ControllRR.Presentation.Controllers;

public class MaintenancesController : Controller
{

    private readonly IMaintenanceService _maintenanceService;
    private readonly IUserService _userService;
    private readonly IDeviceService _deviceService;

    public MaintenancesController(IMaintenanceService maintenanceService, IUserService userService, IDeviceService deviceService, ControllRRContext controllRRContext)
    {
        _maintenanceService = maintenanceService;
        _userService = userService;
        _deviceService = deviceService;

    }

    public async Task<IActionResult> Index()
    {
        var obj = await _maintenanceService.FindAllAsync();
        return View(obj);
    }

    public async Task<IActionResult> Details(int id)
    {
        // PAREI AQUI var obj = new MaintenanceViewModel { Maintenance = }
        var list = await _maintenanceService.FindByIdAsync(id);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "ID não pode ser nulo!" });
        }
        if (list == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado!" });
        }
        return View(list);

    }
    [HttpGet]
    public async Task<IActionResult> New()
    {

        var users = await _userService.FindAllAsync();
        var userDto = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Phone = u.Phone,
            Register = u.Register

        }).ToList();

        var device = await _deviceService.FindAllAsync();
        var deviceDto = device.Select(d => new DeviceDto
        {
            Id = d.Id,
            Type = d.Type,
            Identifier = d.Identifier,
            Model = d.Model,
            SerialNumber = d.SerialNumber,
            DeviceDescription = d.DeviceDescription,
            SectorId = d.SectorId
        });

        var viewModel = new MaintenanceViewModel { UserDto = userDto, DeviceDto = device };
        return View(viewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(MaintenanceDto maintenanceDto)
    {
        if (!ModelState.IsValid)
        {


            var user = await _userService.FindAllAsync();
            var userDto = user.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Register = u.Register

            }).ToList();
            var device = await _deviceService.FindAllAsync();
            var viewModel = new MaintenanceViewModel { MaintenanceDto = maintenanceDto, UserDto = userDto, DeviceDto = device };


            return View(viewModel);
        }


        await _maintenanceService.InsertAsync(maintenanceDto);
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> MaintenanceList()
    {
        return View();
    }
    [HttpPost]
    public async Task<JsonResult> AllMaintenances()//
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"].FirstOrDefault();
        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        var result = await _maintenanceService.GetMaintenanceDataTableAsync(
            start, length, searchValue, sortColumn, sortDirection);

        return Json(result);
    } // Fim do método

    [HttpGet]
    public async Task<IActionResult> ChangeMaintenance(int? id)
    {

        var maintenance = await _maintenanceService.FindByIdAsync(id.Value);


        var device = await _deviceService.FindByIdAsync(maintenance.Device.Id);
        var users = await _userService.FindAllAsync();
        var userDto = users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Phone = u.Phone,
            Register = u.Register

        }).ToList();
        MaintenanceViewModel viewModel = new MaintenanceViewModel { UserDto = userDto, MaintenanceDto = maintenance };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeMaintenance(int? id, MaintenanceDto maintenanceDto)
    {
        if (!ModelState.IsValid)
        {
            var users = await _userService.FindAllAsync();
            var userDto = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Register = u.Register

            }).ToList();

            MaintenanceViewModel viewModel = new MaintenanceViewModel { UserDto = userDto, MaintenanceDto = maintenanceDto };
            return View(viewModel);
        }

        try
        {
            await _maintenanceService.UpdateAsync(maintenanceDto);
            return RedirectToAction(nameof(MaintenanceList));
        }
        catch (ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }

    }


    public async Task<IActionResult> Print(int id)
    {
        var list = await _maintenanceService.FindByIdAsync(id);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Não foi possivel determinar a Ordem de serviço para impressão" });
        }
        if (list == null)
        {
            return RedirectToAction(nameof(Error), new { message = "A ordem de serviço não existe ou existe um erro na consulta" });
        }

        return View(list);
    }

    public async Task<IActionResult> Finalize(int id)
    {
        try
        {
            var maintenance = await _maintenanceService.FindByIdAsync(id);
            if (maintenance == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Manuteção não encontrada! Impossivel Finalizar uma manutenção inexistente!" });
            }
            await _maintenanceService.FinalizeAsync(id);
            return RedirectToAction(nameof(Index));

        }
        catch (ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { e.Message });
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _maintenanceService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (IntegrityException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }
    }

    public async Task<IActionResult> Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }
    public void MeuDebug(string xx)
    {
        Console.WriteLine(xx);

    }

}
