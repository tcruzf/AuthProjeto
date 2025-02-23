using System.Diagnostics;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class SectorsController : Controller
{
    private readonly ISectorService _sectorService;
    public SectorsController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }


    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateNewSector()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewSector(SectorDto sectorDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["SuccessMessage"] = "Setor inserido com sucesso!";
            return View(sectorDto);
        }
        await _sectorService.InsertAsync(sectorDto);
        return RedirectToAction("GetAll");

    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> GetList()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "10");
        var searchValue = Request.Form["search[value]"].FirstOrDefault()?.ToLower();
        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][data]"].FirstOrDefault();
        var sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

        var result = await _sectorService.GetSectorAsync(
            start, length, searchValue, sortColumn, sortDirection);

        return Json(result);
    }
    //
    
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> SectorDetails(int id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Não foi fornecido um id valido!" });
        }

        var sector = await _sectorService.FindByIdAsync(id);
        if (sector == null)
        {
            return RedirectToAction(nameof(Error), new { messgae = "O setor informado não foi encontrado." });
        }
        return View(sector);

    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> SectorEdit(int id)
    {
        var sector = await _sectorService.FindByIdAsync(id);
        return View(sector);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> SectorEdit(int? id, SectorDto sectorDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Setor não pode ser alterado";
            var sectorView = await _sectorService.FindByIdAsync(id.Value);
            return View(sectorView);
        }
        try
        {
            await _sectorService.UpdateAsync(sectorDto);
            TempData["SuccessMessage"] = "Setor alterado com sucesso.";
            return RedirectToAction(nameof(GetAll));

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