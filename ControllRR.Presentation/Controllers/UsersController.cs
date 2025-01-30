/*
    UsersController
    Responsavel por lidar com a interação e ações relacionadas aos usuarios 
    
*/
using System.Diagnostics;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx;

namespace ControllRR.Presentation.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
 

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Index()
    {
        var user = await _userService.FindAllAsync();
        return View("Views/Users/GetAll.cshtml", user);
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.FindAllAsync();
        return View(users);
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> UserDetails(int id)
    {
        var user = await _userService.FindByIdAsync(id);
        return View(user);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateNewUser()
    {
        //_roleManager.ToString();
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewUser(ApplicationUserDto userDto)
    {
        
        /*
        //_userManager.CreateAsync(userDto);
        if (!ModelState.IsValid)
        {
            TempData["SuccessMessage"] = "Usuário Não foi inserio!";
            return View(userDto);
        } */
        try
        {
            await _userService.InsertAsync(userDto);
            TempData["SuccessMessage"] = "Usuário Inserido com sucesso.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        return RedirectToAction(nameof(GetAll));

    }

    [Authorize(Roles = "Admin")]
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

    [HttpGet]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> ChangeUser(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Error), new { message = "O id fornecido não é valido!" });
        }
        var user = await _userService.FindByIdAsync(id.Value);
        if (user == null)
        {
            return RedirectToAction(nameof(Error), new { message = "Usuario não encontrado!" });
        }
        return View(user);

    }


    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> ChangeUser(int? id, ApplicationUserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["SuccessMessage"] = "Usuario não pode ser alterado!";
            var userData = await _userService.FindByIdAsync(id.Value);
            return View(userData);
        }

        try
        {
            await _userService.UpdateAsync(userDto);
            TempData["SuccessMessage"] = "Usuario alterado com sucesso!";
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