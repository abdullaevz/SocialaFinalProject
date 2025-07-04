using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Features.Commands.UserBlockCommands.CreateUserBlock;
using SocialApp.APPLICATION.Features.Commands.UserBlockCommands.RemoveUserBlock;
using SocialApp.APPLICATION.Features.Commands.UserCommands.ReportUser;
using SocialApp.APPLICATION.Features.Commands.UserFriendCommands.CreateUserFriendCommand;
using SocialApp.APPLICATION.Features.Commands.UserFriendCommands.DeleteUserFriendCommand;
using SocialApp.APPLICATION.Features.Commands.UserFriendCommands.UpdateUserFriendRequestCommand;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetCurrentUsers;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetSuggestedFriends;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendRequestsQueryRequest;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendStatusQuery;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetActiveBanner;
using SocialApp.APPLICATION.Features.Queries.PostLikeQueries.CheckPostLike;
using SocialApp.APPLICATION.Features.Queries.PostLikeQueries.GetLikedPostsByUserId;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllUserPostsById;
using SocialApp.APPLICATION.Features.Queries.PostSaveQueries.CheckSavedPosts;
using SocialApp.APPLICATION.Features.Queries.PostSaveQueries.GetSavedPostsByUserId;
using SocialApp.APPLICATION.Features.Queries.SocialAccountQueries.SocialAcountsGetAll;
using SocialApp.APPLICATION.Features.Queries.UserBlockQueries.CheckBlockedUsers;
using SocialApp.APPLICATION.Features.Queries.UserQueries.GetUserByUsername;
using SocialApp.APPLICATION.ViewModels.UserBlockViewModels;
using SocialApp.APPLICATION.ViewModels.UserFriendViewModels;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models.IdentityModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
[Route("[controller]")]
public class UserFriendController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public UserFriendController(UserManager<AppUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet("ExploreFriends")]
    public async Task<IActionResult> ExploreFriends()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        var result = await _mediator.Send(new SuggestedFriendsQueryRequest(user.Id));
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(new SuggestedFriends() { SuggestedList = result.Data });
    }

    [HttpGet("GetUserProfile/{username}")]
    public async Task<IActionResult> GetUserProfile(string username)
    {


        var user = await _userManager.GetUserAsync(User);
        if (user is null) { return RedirectToAction("Login", "Account"); }

        var userResult = await _mediator.Send(new GetUserByUsernameQueryRequest(username, user.Id));
        if (!userResult.Success)
        {
            foreach (var item in userResult.Errors)
            {
                TempData["Error"] = item;
            }
            ViewBag.LocalUsername = user.UserName;

            return RedirectToAction("ExploreFriends", "UserFriend");
        }

        var accResult = await _mediator.Send(new SocialAccountsQueryRequest(userResult.OneData.Id));
        var postResult = await _mediator.Send(new GetAllUserPostsQueryRequest(userResult.OneData.Id));
        var friendStatus = await _mediator.Send(new GetUsersFriendStatusQueryRequest(user.Id, userResult.OneData.Id));
        var likesList = await _mediator.Send(new CheckPostLikeQueryRequest(userResult.OneData.Id));
        var savesList = await _mediator.Send(new CheckSavedPostsCommandRequest(userResult.OneData.Id));
        var likedPostFullList = await _mediator.Send(new GetLikedPostsByUserIdRequest(userResult.OneData.Id));
        var savedPostFullList = await _mediator.Send(new GetSavedPostsByUserIdRequest(userResult.OneData.Id));
        var banner = await _mediator.Send(new GetActiveBannerQueryRequest());
        var blockList = await _mediator.Send(new CheckBlockedUsersByIdRequest(userResult.OneData.Id));
        var friendsList = await _mediator.Send(new GetCurrentUsersQueryRequest(user.Id));
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(new UserProfileVM() { CurrentFriends = friendsList.Data, ActiveBanner = banner.OneData.Url, BlockIds = blockList.Data, OtherLikedPosts = likedPostFullList.Data, OtherSavedPosts = savedPostFullList.Data, SavedPosts = savesList.Data, LikedPosts = likesList.Data, LocalUser = user, AreFriends = friendStatus.Response, User = userResult.OneData, Accounts = accResult.Data, Posts = postResult.Data, LocalUsername = user.UserName });
    }



    [HttpGet("SendFriendRequest/{userId}")]
    public async Task<IActionResult> SendFriendRequest(int userId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(ExploreFriends));
        }
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }

        var requestResult = await _mediator.Send(new CreateUserFriendCommandRequest(new UserFriendCreateVM()
        {
            SenderId = user.Id,
            RecieverId = userId
        }));

        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;

        return RedirectToAction(nameof(ExploreFriends));
    }

    [HttpGet("RequestResult/{result}/{requestId}")]
    public async Task<IActionResult> RequestResult(Statuses result, int requestId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        await _mediator.Send(new UpdateUserFriendRequestCommandRequest(result, requestId, user.Id));
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("RemoveFriend/{userId}")]
    public async Task<IActionResult> RemoveFriend(int userId)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login","Account");
        }

        var referer = Request.Headers["Referer"].ToString();

        var resul = await _mediator.Send(new DeleteUserFriendCommandRequest(user.Id,userId));

        if (referer is not null)
        {
            return Redirect(referer);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("AllRequests")]
    public async Task<IActionResult> AllRequests()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        var list = await _mediator.Send(new RecievedFriendRequestsQueryRequest(user.Id));
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(list.Data);
    }

    [HttpGet("CurrentFriends")]
    public async Task<IActionResult> CurrentFriends()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        var list = await _mediator.Send(new GetCurrentUsersQueryRequest(user.Id));

        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(list.Data);
    }

    [HttpGet("BlockUser/{userId}/{targetId}")]
    public async Task<IActionResult> BlockUser(int userId, int targetId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        if (userId == user.Id)
        {
            var result = await _mediator.Send(new AddBlockCommandRequest(new CreateBlockVM()
            {
                BlockedBy = userId,
                BlockedId = targetId,
            }));
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return Redirect(referer);
    }

    [HttpGet("RemoveBlockUser/{userId}/{targetId}")]
    public async Task<IActionResult> RemoveBlockUser(int userId, int targetId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        if (userId == user.Id)
        {
            var result = await _mediator.Send(new RemoveUserBlockCommandRequest(new CreateBlockVM()
            {
                BlockedBy = userId,
                BlockedId = targetId,
            }));
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return Redirect(referer);
    }

    [HttpGet("Reportuser/{userId}/{targetId}")]
    public async Task<IActionResult> Reportuser(int userId, int targetId)
    {
        var referer = Request.Headers["Referer"].ToString();
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        if (userId == user.Id)
        {
            var result = await _mediator.Send(new ReportUserCommandsRequest(new UserReportVM()
            {
                ReporterId = userId,
                UserId = targetId,
                Status = SecurityStatuses.SUSPICIOUS
            }));
        }
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return Redirect(referer);
    }
}
