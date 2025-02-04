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
    private readonly IStockService _stockService;

    public MaintenancesController(
     IMaintenanceService maintenanceService,
     IUserService userService,
     IDeviceService deviceService,
     ControllRRContext controllRRContext,
     IStockService stockService
      )
    {
        _maintenanceService = maintenanceService;
        _userService = userService;
        _deviceService = deviceService;
        _stockService = stockService;

    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Index()
    {
        var obj = await _maintenanceService.FindAllAsync();
        return View(obj);
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Details(int id)
    {
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
    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> New()
    {
        // Busca usuários
        var users = await _userService.FindAllAsync();
        //System.Console.WriteLine(users.);
        var applicationUserDto = users?
            .Select(u => new ApplicationUserDto
            {
                Id = u.Id,
                Name = u.Name,
                Phone = u.Phone,
                Register = u.Register
            })
            .ToList() ?? new List<ApplicationUserDto>(); 

        // Busca dispositivos e mapeia para DTO
        var devices = await _deviceService.FindAllAsync();
        var deviceDto = devices?
            .Select(d => new DeviceDto
            {
                Id = d.Id,
                Type = d.Type,
                Identifier = d.Identifier,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                DeviceDescription = d.DeviceDescription,
                SectorId = d.SectorId
            })
            .ToList() ?? new List<DeviceDto>(); 

        var viewModel = new MaintenanceViewModel
        {
            ApplicationUserDto = users,
            DeviceDto = deviceDto,
            AvailableStocks = await _stockService.FindAllAsync()
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> New(MaintenanceDto maintenanceDto)
    {
        if (maintenanceDto.ApplicationUserId != null)
        {
            System.Console.WriteLine("Id não é nulo:");
            System.Console.WriteLine(maintenanceDto.ApplicationUserId);
        }

        System.Console.WriteLine(maintenanceDto.Id);
        System.Console.WriteLine(maintenanceDto.Status);
        System.Console.WriteLine(maintenanceDto.ApplicationUserId);
        //System.Console.WriteLine(maintenanceDto.ApplicationUser.Name);


        var user = await _userService.FindAllAsync();
        var applicationUserDto = user.Select(u => new ApplicationUserDto
        {
            Name = u.Name,
            Phone = u.Phone,
            Register = u.Register

        }).ToList();
        await _maintenanceService.InsertAsync(maintenanceDto);


        return RedirectToAction(nameof(MaintenanceList));

    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> MaintenanceList()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
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
    }
    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> ChangeMaintenance(int? id)
    {
        var maintenance = await _maintenanceService.FindByIdAsync(id.Value);
        var device = await _deviceService.FindByIdAsync(maintenance.Device.Id);
        var users = await _userService.FindAllAsync();
        
       
        MaintenanceViewModel viewModel = new MaintenanceViewModel
         { 
          ApplicationUserDto = users,
          MaintenanceDto = maintenance
          
           };
        return View(viewModel);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeMaintenance(int? id, MaintenanceDto maintenanceDto)
    {
        System.Console.WriteLine($"Id: {maintenanceDto.Id}");
            System.Console.WriteLine($"MaintenanceNumber: {maintenanceDto.MaintenanceNumber}");
            System.Console.WriteLine($"OpenDate: {maintenanceDto.OpenDate}");
            System.Console.WriteLine($"SimpleDesc: {maintenanceDto.SimpleDesc}");
            System.Console.WriteLine($"CloseDate: {maintenanceDto.CloseDate}");
            System.Console.WriteLine($"ApplicationUserId: {maintenanceDto.ApplicationUserId}");
            System.Console.WriteLine($"DeviceId: {maintenanceDto.DeviceId}");
            System.Console.WriteLine($"Status: {maintenanceDto.Status}");
            System.Console.WriteLine($"Description: {maintenanceDto.Description}");

            // Produtos
            if (maintenanceDto.MaintenanceProducts != null)
            {
                System.Console.WriteLine("MaintenanceProducts:");
                foreach (var product in maintenanceDto.MaintenanceProducts)
                {
                    System.Console.WriteLine($"  StockId: {product.StockId}, QuantityUsed: {product.QuantityUsed}");
                }
            }
            else
            {
                System.Console.WriteLine("MaintenanceProducts: null");
            }

            // Valores que aparecem no form mas não estão sendo capturados
            System.Console.WriteLine("Hidden/Implicit Values:");
            System.Console.WriteLine($"Device (object): {maintenanceDto.Device}");
            System.Console.WriteLine($"ApplicationUser (object): {maintenanceDto.ApplicationUser}");
            
        if(!ModelState.IsValid)
        {
            var users = await _userService.FindAllAsync();
            var applicationUserDtos = _userService.FindAllAsync();
            var devices = _deviceService.FindAllAsync();

            MaintenanceViewModel viewModel = new MaintenanceViewModel {
                ApplicationUserDto = users,
                MaintenanceDto = maintenanceDto
                
            };
        }
                
        try
        {
            await _maintenanceService.UpdateAsync(maintenanceDto);
            TempData["SuccessMessage"] = "Manutenção alterada com sucesso.";
            return RedirectToAction(nameof(MaintenanceList));
        }
        catch (ApplicationException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }

    }

    [Authorize(Roles = "Manager, Admin")]
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

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {

            await _maintenanceService.RemoveAsync(id);
            return RedirectToAction(nameof(UsersController.Index));
        }
        catch (IntegrityException e)
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

    [Authorize(Roles = "Manager, Admin")]
    public void MeuDebug(string xx)
    {
        Console.WriteLine(xx);

    }

    [Authorize(Roles = "Manager, Admin")]
    private async Task<MaintenanceViewModel> CarregarViewModel()
    {
        return new MaintenanceViewModel
        {
            ApplicationUserDto = await _userService.FindAllAsync(),
            DeviceDto = await _deviceService.FindAllAsync(),
            AvailableStocks = await _stockService.FindAllAsync()
        };
    }

}
