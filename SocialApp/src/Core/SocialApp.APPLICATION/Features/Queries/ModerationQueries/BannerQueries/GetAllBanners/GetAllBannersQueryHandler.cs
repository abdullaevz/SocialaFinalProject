using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.BannerQueries.GetAllBanners;

public class GetAllBannersQueryHandler : IRequestHandler<GetAllBannersQueryRequest, GenericAppResult<Banner>>
{
    private readonly IReadRepository<Banner> _readRepository;

    public GetAllBannersQueryHandler(IReadRepository<Banner> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GenericAppResult<Banner>> Handle(GetAllBannersQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _readRepository.GetAll().OrderByDescending(b=>b.IsActive);

        if (query is null)
        {
            return await GenericAppResult<Banner>.Failure("Banner is null from the database");
        }

        return new GenericAppResult<Banner>() { Success = true, Data = query.ToList() };
    }
}
