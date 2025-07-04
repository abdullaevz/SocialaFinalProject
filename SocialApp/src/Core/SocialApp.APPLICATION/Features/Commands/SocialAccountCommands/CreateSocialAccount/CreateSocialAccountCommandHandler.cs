using AutoMapper;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.SocialAccounts.CreateSocialAccount;

public class CreateSocialAccountCommandHandler : IRequestHandler<CreateSocialAccountCommandRequest, AppResult>
{
    private readonly IWriteRepository<SocialAccount> _writeRepo;

    public CreateSocialAccountCommandHandler(IWriteRepository<SocialAccount> writeRepo, IMapper mapper)
    {
        _writeRepo = writeRepo;
    }

    public async Task<AppResult> Handle(CreateSocialAccountCommandRequest request, CancellationToken cancellationToken)
    {
        string[] socialAccs = { "Facebook", "Instagram", "Github", "StackOverFlow" };

        foreach (var name in socialAccs)
        {
            await _writeRepo.CreateAsync(new SocialAccount() {UserId =request.UserId,Name=name,CreationDate=DateTime.UtcNow });
        }
        
        return await AppResult.SuccessResult("Created successfully");
    }
}
