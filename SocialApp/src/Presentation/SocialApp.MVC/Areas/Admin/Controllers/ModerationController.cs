using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Features.Commands.ModerationCommands.AuthRestricCommands.UpdateAuthRestrict;
using SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.CreateBanner;
using SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.DeleteBanner;
using SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.SelectBanner;
using SocialApp.APPLICATION.Features.Commands.UserCommands.CreateUser.CreateUserByAdmin;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckLogin;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckRegister;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetAllBanners;
using SocialApp.APPLICATION.ViewModels.AuthResctricViewModels;
using SocialApp.APPLICATION.ViewModels.BannerViewModels;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Areas.Admin.Controllers;


[Authorize(Roles = "Admin")]
[Area("Admin")]
public class ModerationController : Controller
{
    private readonly IMediator _mediator;

    public ModerationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> AuthView()
    {
        var loginStatus = await _mediator.Send(new CheckLoginQueryRequest());
        var registerStatus = await _mediator.Send(new CheckRegisterQueryRequest());
        return View(new AuthModeViewModel() { LoginActionIsActive = loginStatus.OneData, RegisterActionIsActive = registerStatus.OneData });
    }

    [HttpPost]
    public async Task<IActionResult> AuthUpdate(AuthModeViewModel authMode)
    {
        var result = await _mediator.Send(new UpdateAuthRestricCommandRequest(authMode));

        return RedirectToAction(nameof(AuthView));
    }

    [HttpGet]
    public async Task<IActionResult> BannersView(AuthModeViewModel authMode)
    {
        var result = await _mediator.Send(new GetAllBannersQueryRequest());
        return View(new BannerVM() { BannerList=result.Data });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBannerStatus(int id,bool isActive)
    {
        var result = await _mediator.Send(new SelectBannerCommandRequest(new SelectBannerVM() { BannerId=id,IsActive=isActive }));

        return RedirectToAction(nameof(BannersView));
    }

    [HttpPost]
    public async Task<IActionResult> UploadBanner(IFormFile file)
    {
        
        var result = await _mediator.Send(new CreateBannerCommandRequest(file));

        return RedirectToAction(nameof(BannersView));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBanner(int id)
    {
        var result = await _mediator.Send(new DeleteBannerCommandRequest(id));
        return RedirectToAction(nameof(BannersView));
    }

    [HttpGet]
    public Task<IActionResult> CreateUser()
    {
        return Task.FromResult<IActionResult>(View());
    }

    [HttpPost]
    [ActionName("CreateUser")]
    public async Task<IActionResult> CreateUserPost(AdminUserCreateVM userCreateVM)
    {
        var result = await _mediator.Send(new CreateUserByAdminCommandRequest(userCreateVM));

        if (!result.Success)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item);
            }
            return View();
        }

        return RedirectToAction(nameof(CreateUser));
    }



}
