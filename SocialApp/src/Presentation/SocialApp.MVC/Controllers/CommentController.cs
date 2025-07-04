using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Features.Commands.CommentCommands;
using SocialApp.APPLICATION.Features.Commands.CommentCommands.CreateComment;
using SocialApp.APPLICATION.Features.Commands.CommentCommands.DeleteComment;
using SocialApp.APPLICATION.ViewModels.CommentViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
public class CommentController : Controller
{

    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public CommentController(IMediator mediator, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentVM commentModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index", "Home");
        }
        var result = await _mediator.Send(new CreateCommentCommandRequest(new CreateCommentVM()
        {
            PostId = commentModel.PostId,
            UserId = commentModel.UserId,
            Content = commentModel.Content
        }));
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var result = await _mediator.Send(new DeleteCommentByIdCommandRequest(commentId));

        if (!result.Success)
        {
        }

        return RedirectToAction("Index", "Home");

    }

}
