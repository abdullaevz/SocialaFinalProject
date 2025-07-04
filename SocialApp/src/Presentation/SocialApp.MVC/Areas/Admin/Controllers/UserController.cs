using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Features.Commands.AppUserCommands.DeleteUser;
using SocialApp.APPLICATION.Features.Commands.UserCommands.UpdateUser.UpdateUserByAdmin;
using SocialApp.APPLICATION.Features.Commands.UserFriendCommands.UpdateUserFriendRequestCommand;
using SocialApp.APPLICATION.Features.Queries.UserQueries.GetAllUsersByCondition;
using SocialApp.APPLICATION.Features.Queries.UserQueries.GetUserById;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(UserFilterVM filter)
    {
        Console.WriteLine(filter.ByRole);
        var result = await _mediator.Send(new GetAllUsersByConditionRequest(filter));
        foreach (var item in result.OneData.Users)
        {
            Console.WriteLine(item.UserName);
        }
        return View(result.OneData);
    }

    [HttpGet("GetUserById/{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {

        var result = await _mediator.Send(new GetUserByIdQueryRequest(userId));
        var user = result.OneData;

        if (user is null)
        {
            ViewBag.Error = "This user is not exists";
            return View();
        }

        UserUpdateVM vm = new UserUpdateVM()
        {
            Id = user.Id,
            Fullname = user.Fullname,
            UserName = user.UserName,
            Email = user.Email,
            ProfilePhotoPath = user.ProfilePhotoPath,
            DefaultProfilePhoto = user.DefaultProfilePath,
            IsPrivate = user.IsPrivate,
            Description = user.Description,
            Country = user.Country,
            PhoneNumber = user.PhoneNumber,
            Profession = user.PhoneNumber,
            IsVerified = user.IsVerified,
            DateTime = user.CreatedDate,
            Status = user.SecurityStatus,
            Role = user.Role,



        };

        return View(new UserVMAdmin()
        {
            UpdateVM = vm,
            Comments = user.Comments.ToList(),
            Posts = user.Posts.ToList(),
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUser(int userId, UserUpdateVM updateVM)
    {
        Console.WriteLine(updateVM.Status);
        var referer = Request.Headers["Referer"].ToString();
        var result = await _mediator.Send(new UserUpdateByAdminCommandRequest(updateVM, userId));
        if (!result.Success)
        {
            TempData["Errors"] = result.Errors[0];
            return Redirect(referer);

        }
        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }

        return RedirectToAction(nameof(GetUserById));
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _mediator.Send(new DeleteUserCommandRequest(userId));
        return RedirectToAction(nameof(GetAllUsers));
    }
}
