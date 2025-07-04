using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPostsByCondition;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPostsByPostId;
using SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllUserPostsById;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class PostController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PostController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPosts(PostFilterVM postFilterVM)
    {
        Console.WriteLine(postFilterVM.ByUser);
        var result = await _mediator.Send(new GetAllPostsByConditionRequest(postFilterVM));
        return View(result.OneData);
    }

    [HttpGet("GetPostById/{postId}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        var result = await _mediator.Send(new GetAllPostsByPostIdRequest(postId));

        foreach (var item in result.OneData.Comments)
        {
            Console.WriteLine(item.Content);
        }

        return View(result.OneData);
    }

}
