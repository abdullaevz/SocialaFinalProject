using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetBannerById;

public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQueryRequest, GenericAppResult<Banner>>
{
    private readonly IReadRepository<Banner> _readRepository;

    public GetBannerByIdQueryHandler(IReadRepository<Banner> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GenericAppResult<Banner>> Handle(GetBannerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var banner = await _readRepository.GetByIdAsync(request.Id);
        if (banner is null)
        {
            return await GenericAppResult<Banner>.Failure("Banner is null from the database");
        }

        return new GenericAppResult<Banner> { Success = true, OneData = banner };
    }
}
