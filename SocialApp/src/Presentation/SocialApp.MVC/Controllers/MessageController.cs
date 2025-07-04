using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialApp.APPLICATION.Features.Queries.MessageQueries.GetAllMessagesBetweenUsers;
using SocialApp.DOMAIN.Models.IdentityModels;
using System.Threading.Tasks;

namespace SocialApp.MVC.Controllers;

[Authorize(Roles = "Member,Admin")]
[Route("api/messages")]
public class MessageController : Controller
{

    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public MessageController(IMediator mediator, UserManager<AppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet("GetAllMessages/{otherUserId}")]
    public async Task<IActionResult> GetAllMessages(int otherUserId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized("Not authoried any user");
        }
        var result = await _mediator.Send(new GetAllMessagesBetweenUsersRequest(user.Id,otherUserId));

        if (!result.Success)
        {
            return BadRequest("Error1");
        }
     
        return Ok(result.Data);
    }
}
