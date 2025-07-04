using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SocialApp.APPLICATION.Features.Commands.SocialAccounts.UpdateSocialAccounts;
using SocialApp.APPLICATION.Features.Commands.UserCommands.ResetPassword;
using SocialApp.APPLICATION.Features.Queries.SocialAccountQueries.SocialAcountsGetAll;
using SocialApp.APPLICATION.ViewModels.DataTransferViewModels;
using SocialApp.APPLICATION.ViewModels.SocialMediaViewModels;
using SocialApp.APPLICATION.ViewModels.StatusViewModels;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.APPLICATION.Features.Commands.UserCommands.UpdateUser;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public UserController(IMediator mediator, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }


    [HttpGet]
    public async Task<IActionResult> Settings()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> ContactSettings()
    {
        var user = await _userManager.GetUserAsync(User);
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View();  }

    [HttpGet]
    public async Task<IActionResult> AccountSettings()
    {
        var user = await _userManager.GetUserAsync(User);
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;

        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(new UserUpdateVM()
        {
            UserName = user.UserName,
            Email = user.Email,
            Fullname = user.Fullname,
            Id = user.Id,
            Country = user.Country,
            Profession = user.Profession,
            PhoneNumber = user.PhoneNumber,
            ProfilePhotoPath = user.ProfilePhotoPath
        });
    }


    [HttpGet]
    public async Task<IActionResult> ResetPassword()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View();
    }

    [HttpPost]
    [ActionName("ResetPassword")]
    public async Task<IActionResult> ResetPassword(UserPasswordUpdateVM userPasswordUpdateVM)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "All fields are required");
            return View(userPasswordUpdateVM);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        userPasswordUpdateVM.UserId = user.Id;
        var result = await _mediator.Send(new UserResetPasswordCommandRequest(userPasswordUpdateVM));

        if (!result.Success)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item);
                return View(userPasswordUpdateVM);
            }
        }

        ModelState.AddModelError("Success", "Password successfully changed");
        return View(userPasswordUpdateVM);
    }

    [HttpGet]
    public async Task<IActionResult> AccountStatus()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login","Account");
        }
        var model = new StatusVM() { AccountStatus = user.SecurityStatus };
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(model);
    }




    [HttpGet]
    public async Task<IActionResult> Update()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;

        return View(new UserUpdateVM()
        {
            UserName = user.UserName,
            Email = user.Email,
            Fullname = user.Fullname,
            Id = user.Id,
            Country = user.Country,
            Profession = user.Profession,
            PhoneNumber = user.PhoneNumber,
            ProfilePhotoPath = user.ProfilePhotoPath,
            Description = user.Description,
            IsPrivate = user.IsPrivate,
            DefaultProfilePhotoPath = user.DefaultProfilePath,
            EmailConfirmed = user.EmailConfirmed

        });
    }

    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> Update(UserUpdateVM userModel)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.LocalUsername = user.UserName;
        var result = await _mediator.Send(new UserUpdateCommandRequest(user, userModel));
        if (!result.Success)
        {

            ViewBag.Errors = result.Errors;
            ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
            userModel.ProfilePhotoPath = user.ProfilePhotoPath;
            userModel.DefaultProfilePhotoPath = user.DefaultProfilePath;
            return View(userModel);
        }


        return RedirectToAction(nameof(Update));
    }


    [HttpGet]
    public async Task<IActionResult> SocialSettings()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction(nameof(Login));
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;

        var listResult = await _mediator.Send(new SocialAccountsQueryRequest(user.Id));

        if (!listResult.Success)
        {
            return View(new SocialAccountVM() { ErrorMessage = "Something went wrong about your social accounts" });
        }

        return View(new SocialAccountVM() { socialAccountsList = listResult.Data, });
    }

    [HttpPost]
    public async Task<IActionResult> SocialAccountUpdate(SocialAccountVM socialAccount)
    {

        var result = await _mediator.Send(new UpdateSocialAccountCommandRequest(socialAccount.socialAccountsList));

        return RedirectToAction(nameof(SocialSettings));
    }
}
