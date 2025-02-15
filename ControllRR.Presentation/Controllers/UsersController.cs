/*
    UsersController
    Responsavel por lidar com a interação e ações relacionadas aos usuarios 
    
*/
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Infrastructure.Exceptions;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Mysqlx;


namespace ControllRR.Presentation.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;


    public UsersController(IUserService userService, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userService = userService;
        _roleManager = roleManager;
        _userManager = userManager;
        _emailSender = emailSender;
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

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateNewUser()
    {
        var roles = await _roleManager.Roles.Select(r => new SelectListItem
        {
            Value = r.Name,
            Text = r.Name
        }).ToListAsync();

        var applicationViewModel = new RoleViewModel
        {
            Roles = roles,

        };

        return View(applicationViewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNewUser(RoleViewModel userDto)
    {
        string returnUrl = "";
        returnUrl ??= Url.Content("~/");
        var adminEmail = userDto.applicationUserDto.Email;
        //var tempRole = userDto.applicationUserDto.Role;
        try
        {
            var user = new ApplicationUser
            {
                OperatorId = null,
                Name = userDto.applicationUserDto.Name,
                Register = userDto.applicationUserDto.Register,
                Phone = null,
                UserName = adminEmail,
                Email = userDto.applicationUserDto.Email,
                EmailConfirmed = true,
                Role = userDto.applicationUserDto.Role
            };

            var result = await _userManager.CreateAsync(user, "Thiago123##");

            if (result.Succeeded)
            {
                // Baseaddo no identity, algumas alterações para que o usuario possa
                // confirmar a conta sem ter que receber o link. Claro, isso é uma gambiarra temporaria.
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Users",
                new { userId = user.Id, code = encodedCode },
                protocol: Request.Scheme);

                TempData["ConfirmationLink"] = callbackUrl;
                return RedirectToAction("ShowConfirmationLink");
            }

            AddErrorsToModelState(result.Errors);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        userDto.Roles = await GetRolesList();
        return View(userDto);
    }

    [HttpGet]
    public IActionResult CheckEmail(string email)
    {
        ViewData["Email"] = email;
        return View();
    }
    private async Task<List<SelectListItem>> GetRolesList()
    {
        return await _roleManager.Roles
            .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
            .ToListAsync();
    }
    [HttpGet]
    public IActionResult ShowConfirmationLink()
    {
        //TempData["ErrorMessage"] = "Parâmetros inválidos para confirmação de e-mail.";
        //TempData["SuccessMessage"] = "Usuario cadastrado! Ative o usuario clicando no link abaixo:";
        var link = TempData["ConfirmationLink"]?.ToString();


        return View();
    }

    private void AddErrorsToModelState(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            TempData["ErrorMessage"] = "Parâmetros inválidos para confirmação de e-mail.";
            return RedirectToAction(nameof(GetAll));
        }

        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Usuário não encontrado.";
                return RedirectToAction(nameof(GetAll));
            }

            var decodedCodeBytes = WebEncoders.Base64UrlDecode(code);
            var decodedCode = Encoding.UTF8.GetString(decodedCodeBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "E-mail confirmado com sucesso!";
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                TempData["ErrorMessage"] = $"Erro na confirmação: {errors}";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erro crítico: {ex.Message}";
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