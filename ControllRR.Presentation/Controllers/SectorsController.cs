using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class SectorsController : Controller
{
    private readonly ISectorService _sectorService;
    public SectorsController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    [HttpGet]
    public async Task<IActionResult> CreateNewSector()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewSector(SectorDto sectorDto)
    {
        if(!ModelState.IsValid)
        {
             TempData["SuccessMessage"] = "Setor inserido com sucesso!";
             return View(sectorDto);
        }
        await _sectorService.InsertAsync(sectorDto);
        return RedirectToAction("GetAll");

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return View();
    }

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

}