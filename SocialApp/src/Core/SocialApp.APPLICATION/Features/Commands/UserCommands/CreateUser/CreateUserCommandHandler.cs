using AutoMapper;
using CloudinaryDotNet;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Features.Commands.SocialAccounts.CreateSocialAccount;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, AppResult>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IWriteRepository<SocialAccount> _socialWriteRepo;
    private readonly IMediator _mediator;
    private readonly string _defaultPP;
    
    public CreateUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> singInManager, IMapper mapper, IWriteRepository<SocialAccount> socialWriteRepo, IMediator mediator,IConfiguration configuration)
    {
        _userManager = userManager;
        _mapper = mapper;
        _socialWriteRepo = socialWriteRepo;
        _mediator = mediator;
        _defaultPP = configuration["DefaultLink:DefaultProfilePhoto"] ?? "empty";
    }

    public async Task<AppResult> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.userModel.Policy is false)
        {
            //return new AppResult() { Success = false, OnceError = "Policy" };
            return await AppResult.Failure("Please Accept Term and Conditions");
        }

        if (request is null)
        {
            throw new UserException("User model is cannot be null");
        }

        var isUser = await _userManager.FindByEmailAsync(request.userModel.Email) ?? await _userManager.FindByNameAsync(request.userModel.Username);

        if (isUser is not null)
        {
            return await AppResult.Failure("This email or username is already registered,try to log in");
        }

        AppUser user = new()
        {
            Email = request.userModel.Email,
            Fullname = request.userModel.Fullname,
            AcceptPolicyStatus = request.userModel.Policy,
            isDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UserName = request.userModel.Username,
            ProfilePhotoPath = "https://res.cloudinary.com/dm7wfyrrf/image/upload/v1749760141/default-pp_grev8x.png",
            DefaultProfilePath = "https://res.cloudinary.com/dm7wfyrrf/image/upload/v1749760141/default-pp_grev8x.png",
            SecurityStatus = DOMAIN.Enums.SecurityStatuses.SUSPICIOUS,
            Role=DOMAIN.Enums.UserRoles.MEMBER,
        };

        var createResult = await _userManager.CreateAsync(user, request.userModel.Password);
     
        if (!createResult.Succeeded)
        {
            return await AppResult.Failure(createResult.Errors.Select(e => e.Description).ToArray());
        }

        //Create social media tables to User
        await _mediator.Send(new CreateSocialAccountCommandRequest(user.Id));

        var addRoleResult = await _userManager.AddToRoleAsync(user, "Member");

        if (!addRoleResult.Succeeded)
        {
            return await AppResult.Failure(addRoleResult.Errors.Select(e => e.Description).ToArray());
        }
       
        return new AppResult() { Success=true,Message="User created and confirmation link sent",User=user };
    }
}
