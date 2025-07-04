using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.SocialAccounts.UpdateSocialAccounts;

public class UpdateSocialAccountCommandRequest : IRequest<AppResult>
{
    public List<SocialAccount> SocialAccounts { get; set; }

    public UpdateSocialAccountCommandRequest(List<SocialAccount> socialAccounts)
    {
        SocialAccounts = socialAccounts;
    }
}

public class UpdateSocialAccountCommandHandler : IRequestHandler<UpdateSocialAccountCommandRequest, AppResult>
{
    private readonly IWriteRepository<SocialAccount> _writeRepository;
    private readonly IReadRepository<SocialAccount> _readRepository;

    public UpdateSocialAccountCommandHandler(IWriteRepository<SocialAccount> writeRepository, IReadRepository<SocialAccount> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(UpdateSocialAccountCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.SocialAccounts is null)
        {
            //EXCEPTION
        }


        foreach (var item in request.SocialAccounts)
        {
            var entity = await _readRepository.GetByIdAsync(item.Id);
            if (entity is not null)
            {
                entity.Url = item.Url;
            }
            await _writeRepository.SaveAsync();
        }


        return await AppResult.SuccessResult("Updated succesfully");
    }
}
