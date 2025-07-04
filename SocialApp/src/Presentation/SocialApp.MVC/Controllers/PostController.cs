using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.Features.Commands.PostCommands.CreatePost;
using SocialApp.APPLICATION.Features.Commands.PostCommands.DeletePost;
using SocialApp.APPLICATION.Features.Commands.PostCommands.UpdatePost;
using SocialApp.APPLICATION.Features.Commands.PostLikeCommands;
using SocialApp.APPLICATION.Features.Commands.PostLikeCommands.AddLike;
using SocialApp.APPLICATION.Features.Commands.PostLikeCommands.RemoveLike;
using SocialApp.APPLICATION.Features.Commands.PostSaveCommands.AddSave;
using SocialApp.APPLICATION.Features.Commands.PostSaveCommands.RemoveSave;
using SocialApp.APPLICATION.Features.Queries.PostLikeQueries.CheckPostLike;
using SocialApp.APPLICATION.ViewModels.DataTransferViewModels;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
public class PostController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> userManager;
    private readonly IWriteRepository<PostLike> writeRepository;
    private readonly IModerationService _moderationService;

    public PostController(IMediator mediator, UserManager<AppUser> userManager, IWriteRepository<PostLike> writeRepository, IModerationService moderationService)
    {
        _mediator = mediator;
        this.userManager = userManager;
        this.writeRepository = writeRepository;
        _moderationService = moderationService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(HomePageVM homePageVM)
    {
        if (homePageVM.PostCreateModel.Content is null)
        {
            throw new PostException("Error:Post model is null");
        }

        AppUser? user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        homePageVM.PostCreateModel.UserId = user.Id;


        try { var contentResult = await _moderationService.SendPrompt(homePageVM.PostCreateModel.Content); }
        catch (Exception ex)
        {
            TempData["WarningMessage"] = "Please make sure that the content you share is ethical and safe.";
            return RedirectToAction("Index", "Home");
        }

        var result = await _mediator.Send(new CreatePostCommandRequest(homePageVM.PostCreateModel));

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        var request = new DeletePostCommandRequest(id);
        _mediator.Send(request);
        return View();
    }

    [HttpPost]
    [Route("Post/UpdatePostLike")]
    public async Task<IActionResult> UpdatePostLike([FromBody] LikeCreateVM likeModel)
    {


        if (likeModel.IsLike)
        {
            var addLike = await _mediator.Send(new AddLikeToPostCommandRequest(likeModel));
        }
        else
        {
            var removeLike = await _mediator.Send(new RemoveLikeFromPostCommandRequest(likeModel));
        }

        return Json(new { success = true });
    }

    [HttpGet("Post/SavePost/{postId}")]
    public async Task<IActionResult> SavePost(int postId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("");
        }


        var saveResult = await _mediator.Send(new SavePostCommandRequest(new CreateUpdatePostSaveVM()
        {
            UserId = user.Id,
            PostId = postId,
            IsSave = true
        }));


        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }


        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Post/RemoveSavePost/{postId}")]
    public async Task<IActionResult> RemoveSavePost(int postId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }


        var saveResult = await _mediator.Send(new RemoveSaveFromPostCommandRequest(new CreateUpdatePostSaveVM()
        {
            UserId = user.Id,
            PostId = postId,
            IsSave = true
        }));


        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("ReportPost/{reporterId}/{postId}")]
    public async Task<IActionResult> ReportPost(int reporterId, int postId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        if (user.Id == reporterId)
        {
            var result = await _mediator.Send(new UpdatePostCommandRequest(new PostUpdateVM() { PostId = postId, Status = DOMAIN.Enums.SecurityStatuses.SUSPICIOUS }));
        }
        return Redirect(referer);
    }

    [HttpGet("DeletePost/{userId}/{postId}")]
    public async Task<IActionResult> DeletePost(int userId, int postId)
    {
        var referer = Request.Headers["Referer"].ToString();
        Console.WriteLine("================");
        Console.WriteLine(referer);
        var user = await userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        if (userId == user.Id)
        {
            var result = await _mediator.Send(new DeletePostCommandRequest(postId));
        }

        return Redirect(referer);
    }
}

