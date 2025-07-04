using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetCurrentUsers;
using SocialApp.APPLICATION.Features.Queries.FriendQueries.GetUsersFriendRequestsQueryRequest;
using SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckLogin;
using SocialApp.APPLICATION.Features.Queries.PostLikeQueries.CheckPostLike;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPosts;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllUserPostsById;
using SocialApp.APPLICATION.Features.Queries.PostSaveQueries.CheckSavedPosts;
using SocialApp.APPLICATION.ViewModels.DataTransferViewModels;
using SocialApp.APPLICATION.ViewModels.MessageBoxViewModels;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.INFRASTRUCTURE.Concretes.Servcies;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
public class HomeController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;

    public HomeController(UserManager<AppUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var loginResult = await _mediator.Send(new CheckLoginQueryRequest());
        Console.WriteLine(loginResult.OneData);
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }

        //var result = await _mediator.Send(new GetAllPostsQueryRequest());

        var friendsResult = await _mediator.Send(new GetCurrentUsersQueryRequest(user.Id));

        var postsResult = await _mediator.Send(new GetAllUserPostsQueryRequest(user.Id));
        List<PostGetVM> posts = postsResult.Data;

        foreach (var item in friendsResult.Data)
        {
            var friendPosts = await _mediator.Send(new GetAllUserPostsQueryRequest(item.FriendId));
            posts.AddRange(friendPosts.Data);
        }
        var list = await _mediator.Send(new RecievedFriendRequestsQueryRequest(user.Id));
        var likesList = await _mediator.Send(new CheckPostLikeQueryRequest(user.Id));
        var savedList = await _mediator.Send(new CheckSavedPostsCommandRequest(user.Id));

        Console.WriteLine(list.Data.Count);
        var model = new HomePageVM()
        {
            SavedPosts = savedList.Data,
            UserFriendPosts = posts,
            LocalUser = user,
            FriendRequests = list.Data,
            LikedPosts = likesList.Data
        };
        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.LocalUsername = user.UserName;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> MessageBox()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        var result = await _mediator.Send(new GetCurrentUsersQueryRequest(user.Id));
        List<ChatUserVM> newList = [];

        foreach (var item in result.Data)
        {
            newList.Add(new ChatUserVM() { Fullname = item.Fullname, Initials = $"{item.Fullname[0]}{item.Fullname[item.Fullname.Length - 1]}", UserId = item.FriendId, UserName = item.Username });
        }

        return View(new ChatBoxVM() { ChatBoxUsers = newList, LocalUserId = user.Id, OnlineUsersList = ChatService.OnlineUsers });
    }

    [HttpGet]
    public async Task<IActionResult> ExplorePostPage()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Login", "Account");
        }
        var allPosts = await _mediator.Send(new GetAllPostsQueryRequest());
        var likedPosts = await _mediator.Send(new CheckPostLikeQueryRequest(user.Id));
        var savedPosts = await _mediator.Send(new CheckSavedPostsCommandRequest(user.Id));

        ViewBag.LocalProfilePhoto = user.ProfilePhotoPath;
        ViewBag.DefaultProfilePhoto = user.DefaultProfilePath;
        ViewBag.LocalUsername = user.UserName;
        return View(new ExplorePostsVM()
        {
            LocalUser = user,
            AllPosts = allPosts.Data,
            Likes = likedPosts.Data,
            Saves = savedPosts.Data
        });
    }

    [HttpGet]
    public IActionResult Search(string username)
    {
        return Redirect($"/UserFriend/GetUserProfile/{username}");
    }
}
