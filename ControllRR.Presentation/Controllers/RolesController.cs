using System.Text.Encodings.Web;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Presentation.Areas.Identity.Pages.Account;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class RolesController : Controller
{

    private readonly IApplicationUserService _applicationUserService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(IApplicationUserService applicationUserService,
     IUserService userService,
      UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ILogger<IApplicationUserService> logger

      )

    {
        _applicationUserService = applicationUserService;
        _userManager = userManager;
        _emailSender = emailSender;
        _roleManager = roleManager;
        _logger = logger;

    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> ListUser()
    {
        var user = await _applicationUserService.FindAllAsync();
        return View(user);

    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> AlterUser(string email)
    {
        var users = await _applicationUserService.GetUserManagerAsync(email);
        return View(users);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> AlterUser(string email, string role)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
        {
            return BadRequest("Email e permissões são obrigatórios.");
        }

        var result = await _applicationUserService.AddUserRoleAsync(email, role);
        if (result)
        {
            return Ok($"Role '{role}' adicionada ao usuário '{email}' com sucesso.");
        }

        return NotFound("Usuário não encontrado ou erro ao adicionar role.");
    }


}