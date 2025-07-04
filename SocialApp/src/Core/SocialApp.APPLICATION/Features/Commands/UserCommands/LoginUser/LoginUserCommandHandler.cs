using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.AppUserCommands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, GenericAppResult<AppUser>>
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<GenericAppResult<AppUser>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.loginModel.UsernameOrEmail) ??
            await _userManager.FindByNameAsync(request.loginModel.UsernameOrEmail);

        if (user is null)
        {
            return await GenericAppResult<AppUser>.Failure("Wrong username or email");
        }

        var confirm = await _userManager.CheckPasswordAsync(user,request.loginModel.Password);
        
        if (!confirm)
        {
            return await GenericAppResult<AppUser>.Failure("Invalid password");
        }
        await _signInManager.SignInAsync(user,isPersistent:request.loginModel.Remember);

        return new GenericAppResult<AppUser>() { Success = true, OneData = user };
    }
}
