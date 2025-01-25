using System.Diagnostics;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace ControllRR.Presentation.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.FindAllAsync();
        return View(users);
    }

    public async Task<IActionResult> UserDetails(int id)
    {
        var user = await _userService.FindByIdAsync(id);
        return View(user);
    }
    [HttpGet]
    public async Task<IActionResult> CreateNewUser()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewUser(UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return View(userDto);
        }
        try{
        await _userService.InsertAsync(userDto);
         TempData["SuccessMessage"] = "Usuário Inserido com sucesso.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        return RedirectToAction(nameof(GetAll));

    }


    public async Task<IActionResult> RemoveUser(int id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction(nameof(Error), new { message = "Você não tem permissão para deletar um usuario" });
        }
        try
        {
            await _userService.RemoveAsync(id);
            TempData["SuccessMessage"] = "Usuário excluído com sucesso.";
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro inesperado ao tentar excluir o usuário.";
        }
        return RedirectToAction("GetAll");

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
}