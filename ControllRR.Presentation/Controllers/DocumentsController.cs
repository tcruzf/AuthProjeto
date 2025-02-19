using System.Diagnostics;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ControllRR.Presentation.Controllers;

public class DocumentsController : Controller
{
    private readonly IDocumentService _documentService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public DocumentsController(IDocumentService documentService, IWebHostEnvironment webHostEnvironment)
    {
        _documentService = documentService;
        _webHostEnvironment = webHostEnvironment;
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> FileUpload()
    {
        var documents = await _documentService.GetAllAsync(); // REcupera os documentos do banco de dados

        return View(documents); // Retonrna a lista de documentos para a view
    }

    // Alterar FileUpload e mover para serviços 
    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FileUpload(IFormFile file, string description)
    {

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Você não anexou nenhum documento! Tente novamente!";
            var documents = await _documentService.GetAllAsync(); // REcupera os documentos do banco de dados
            return View(documents);

        }

        var documentDto = new DocumentDto
        {
            FormFile = file,
            Description = description,
            DocumentName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName)
        };

        try
        {
            await _documentService.UploadDocumentAsync(documentDto);
            TempData["SuccessMessage"] = "Documento carregado com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao fazer upload do arquivo: {ex.Message}";
        }

        return RedirectToAction("FileUpload");

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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _documentService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Documento deletado com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro ao deletar documento: {ex.Message}";
        }
        return RedirectToAction("FileUpload");
    }
}