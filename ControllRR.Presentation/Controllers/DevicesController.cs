using System.Diagnostics;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Infrastructure.Data.Context;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ControllRR.Application.Dto;
using Microsoft.AspNetCore.Authorization;

namespace ControllRR.Presentation.Controllers;
public class DevicesController : Controller
{

    private readonly IDeviceService _deviceService;
    private readonly ISectorService _sectorService;
    private readonly ControllRRContext _controllRRContext;

    public DevicesController(IDeviceService deviceService, ISectorService sectorService, ControllRRContext controllRRContext)
    {
        _deviceService = deviceService;
        _sectorService = sectorService;
        _controllRRContext = controllRRContext;
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Index()
    {
        var devices = await _deviceService.FindAllAsync();
        return View(devices);
    }

    public async Task<IActionResult> List()
    {

        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<JsonResult> GetList()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"].FirstOrDefault();
        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        var result = await _deviceService.GetDeviceAsync(
            start, length, searchValue, sortColumn, sortDirection);

        return Json(result);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }

        var devices = await _controllRRContext.Devices
            .Where(d => d.Model.Contains(term) || d.SerialNumber.Contains(term) || d.Type.Contains(term) || d.Identifier.Contains(term))
            .Select(d => new { d.Id, d.Model, d.SerialNumber, d.Type, d.Identifier })
            .ToListAsync();

        return Json(devices);
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Details(int id)
    {
        var device = await _deviceService.FindByIdAsync(id);

        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });
        }
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Identificador não encontrado (xmERR)!" });
        }
        return View(device);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> GetMaintenances(int id)
    {

        var device = await _deviceService.GetMaintenancesAsync(id);


        if (device == null)
        {
            System.Console.WriteLine("Device é null aqui!");
        }
        System.Console.WriteLine(device);
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Id não fornecido!" });
        }
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Dispositivo não encontrado @!" });
        }
        return View(device);

    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateNew()
    {
        var sectors = await _sectorService.FindAllAsync();
        var viewModel = new DeviceViewModel { Sector = sectors };
        return View(viewModel);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNew(DeviceDto deviceDto)
    {
        System.Console.WriteLine(deviceDto);
        if (!ModelState.IsValid)
        {
            var sector = await _sectorService.FindAllAsync();
            var viewModel = new DeviceViewModel { Sector = sector };
            return View(viewModel);
        }
        await _deviceService.InsertAsync(deviceDto);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Não foi fornecido nenhum id para ser editado!" });
        }
        var device = await _deviceService.FindByIdAsync(id.Value);
        if (device == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Dispositivo não encontrado - Erro" });
        }

        List<SectorDto> sector = await _sectorService.FindAllAsync();
        DeviceViewModel viewModel = new DeviceViewModel { Sector = sector, DeviceDto = device };
        return View(viewModel);
    }
    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, DeviceDto deviceDto)
    {
        if (!ModelState.IsValid)
        {
            List<SectorDto> sector = await _sectorService.FindAllAsync();
            var viewModel = new DeviceViewModel { DeviceDto = deviceDto, Sector = sector };
            return View(viewModel);
        }
        if (id != deviceDto.Id)
        {
            return RedirectToAction(nameof(Error), new { message = "Id fornecido não corresponde ao id do dispositivo!" });
        }
        try
        {
            _deviceService.UpdateAsync(deviceDto);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }


    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
    }
}