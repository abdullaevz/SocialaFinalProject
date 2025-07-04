using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SocialApp.APPLICATION.Features.Commands.UserCommands.CreateUser;
using SocialApp.APPLICATION.Features.Commands.AppUserCommands.LoginUser;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckLogin;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckRegister;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Models.IdentityModels;
using System.Net;
using System.Text;

namespace SocialApp.MVC.Controllers;


public class AccountController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IEmailSender _emailService;
    private readonly string _clientDomain;


    public AccountController(IConfiguration configuration, IMediator mediator, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailSender emailService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _clientDomain = configuration["ClientDomain"] ?? "empty";
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var user = await _userManager.GetUserAsync(User);

        return View();
    }

    [HttpGet]
    public Task<IActionResult> Register()
    {


        return Task.FromResult<IActionResult>(View());
    }

    [HttpPost]
    [ActionName("Login")]
    public async Task<IActionResult> LoginPost(UserLoginVM loginModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }



        var result = await _mediator.Send(new LoginUserCommandRequest(loginModel));

        if (!result.Success)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item);
            }
            return View();
        }
        var loginResult = await _mediator.Send(new CheckLoginQueryRequest());
        if (!loginResult.OneData && !(result.OneData.Role == DOMAIN.Enums.UserRoles.ADMIN))
        {
            ModelState.AddModelError(string.Empty, "Access to our website is temporarily unavailable.");
            return View();
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpPost]
    [ActionName("Register")]
    public async Task<IActionResult> RegisterPost(UserCreateVM userModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var registerResult = await _mediator.Send(new CheckRegisterQueryRequest());

        if (!registerResult.OneData)
        {
            ModelState.AddModelError(string.Empty, "Register to our website is temporarily unavailable.");
            return View();
        }

        var result = await _mediator.Send(new CreateUserCommandRequest(userModel));

        if (!result.Success)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item);
            }
            return View();
        }
        if (result.User is not null)
        {
            var generatedToken = await _userManager.GenerateEmailConfirmationTokenAsync(result.User);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(generatedToken));

            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            var confirmationUrl = $"{_clientDomain}Account/ConfirmEmail?userId={result.User.Id}&token={encodedToken}";


            //var confirmLink = Url.Action("ConfirmEmail", "Account", new { userId = result.User.Id, token = encodedToken }, Request.Scheme);
            string htmlMessage = $"Pleace click here <a href='{confirmationUrl}'>for confirm your current email adress</a>.";
            await _emailService.SendEmailAsync(result.User.Email, "Confirm email adress", htmlMessage);
        }


        return RedirectToAction(nameof(Index), "Home");
    }
    [HttpGet]
    public async Task<IActionResult> SendConfirmationEmail()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is not null)
        {
            var generatedToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(generatedToken));

            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            var confirmationUrl = $"{_clientDomain}Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";


            //var confirmLink = Url.Action("ConfirmEmail", "Account", new { userId = result.User.Id, token = encodedToken }, Request.Scheme);
            string htmlMessage = $"Pleace click here <a href='{confirmationUrl}'>for confirm your current email adress</a>.";
            await _emailService.SendEmailAsync(user.Email, "Confirm email adress", htmlMessage);
        }
        else
        {
            RedirectToAction("Login", "Account");
        }

        return RedirectToAction("Update", "User");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {

        if (userId is null || token is null)
            return RedirectToAction(nameof(ExpiredLink));


        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return RedirectToAction("Login", "Account");
        try
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                user.SecurityStatus = DOMAIN.Enums.SecurityStatuses.SAFE;
                return View("ConfirmEmail");
            }
            else
            {
                return RedirectToAction(nameof(ExpiredLink));
            }
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(ExpiredLink));

        }
    }

    [HttpGet]
    public IActionResult ExpiredLink() { return View(); }


    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }


    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    //
    [HttpGet]
    public IActionResult Forgot()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SendPasswordResetLink(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = $"{_clientDomain}Account/ResetOldPassword/{encodedToken}/{user.Email}";

            await _emailService.SendEmailAsync(user.Email, "Reset Your Password",
    $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");
        }
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("Account/ResetOldPassword/{token}/{email}")]
    public async Task<IActionResult> ResetOldPassword(string token, string email)
    {
        if (token is null)
        {
            return View("ExpiredLink");
        }

        return View(new PasswordResetVM() { Token = token, Email = email });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordPost(PasswordResetVM passwordReset)
    {

        if (!passwordReset.NewPassword.Equals(passwordReset.ConfirmPassword))
        {
            ModelState.AddModelError(string.Empty, "Passwords dont match");
            return View();
        }

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(passwordReset.Token));
        if (passwordReset.Token is null)
        {
            return View("ExpiredLink");
        }
        var user = await _userManager.FindByEmailAsync(passwordReset.Email);

        if (user is not null)
        {
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, passwordReset.NewPassword);
            if (!result.Succeeded)
            {
                return View("ExpiredLink");
            }
        }

        return RedirectToAction(nameof(Login));
    }
}
