using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.SocialAccountQueries.SocialAcountsGetAll;

public class SocialAccountsQueryHandler : IRequestHandler<SocialAccountsQueryRequest, GenericAppResult<SocialAccount>>
{
    private readonly IReadRepository<SocialAccount> _readRepository;

    public SocialAccountsQueryHandler(IReadRepository<SocialAccount> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GenericAppResult<SocialAccount>> Handle(SocialAccountsQueryRequest request, CancellationToken cancellationToken)
    {
        var model = _readRepository.GetByCondition(item => item.UserId == request.UserId);

        if (model is null)
        {
            return new GenericAppResult<SocialAccount> {Message= $"Cannot find any entity with this id {request}", Success=false,Data=null };
        }

        return new GenericAppResult<SocialAccount> {Success=true,Data=model.ToList() };
    }
}