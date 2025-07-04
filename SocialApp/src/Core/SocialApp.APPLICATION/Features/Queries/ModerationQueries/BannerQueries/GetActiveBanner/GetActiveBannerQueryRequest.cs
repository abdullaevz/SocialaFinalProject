using MediatR;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetActiveBanner;

public class GetActiveBannerQueryRequest : IRequest<GenericAppResult<Banner>>
{

}

public class GetActiveBannerQueryHandler : IRequestHandler<GetActiveBannerQueryRequest, GenericAppResult<Banner>>
{
    private readonly IReadRepository<Banner> _readRepository;

    public GetActiveBannerQueryHandler(IReadRepository<Banner> readRepository,IConfiguration configuration)
    {
        _readRepository = readRepository;
    }

    public async Task<GenericAppResult<Banner>> Handle(GetActiveBannerQueryRequest request, CancellationToken cancellationToken)
    {
        var banner = _readRepository.GetByCondition(b => b.IsActive == true)?.FirstOrDefault();
        if (banner is null)
        {
            return await GenericAppResult<Banner>.Failure("Not banner found on database"); 
        }

        return new GenericAppResult<Banner>() { Success = true, OneData = banner };
    }
}
