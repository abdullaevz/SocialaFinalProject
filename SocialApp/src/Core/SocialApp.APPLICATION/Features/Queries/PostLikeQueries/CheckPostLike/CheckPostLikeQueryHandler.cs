using AutoMapper;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Queries.PostLikeQueries.CheckPostLike;

public class CheckPostLikeQueryHandler : IRequestHandler<CheckPostLikeQueryRequest, GenericAppResult<LikesVM>>
{
    private readonly IReadRepository<PostLike> _postReadRepository;
    private readonly IMapper _mapper;
    public CheckPostLikeQueryHandler(IReadRepository<PostLike> readRepository, IMapper mapper)
    {
        _postReadRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<LikesVM>> Handle(CheckPostLikeQueryRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new PostLikeException("Like request model is null");
        }

        var likesList = _postReadRepository.GetByCondition(p=>p.UserId==request.UserId)?.ToList();
        List<LikesVM> likedPosts= _mapper.Map<List<LikesVM>>(likesList);
        return new GenericAppResult<LikesVM>() { Success = true, Data = likedPosts };
    }
}
