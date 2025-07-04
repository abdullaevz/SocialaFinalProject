using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllUserPostsById;

public class GetAllUserPostsQueryRequest : IRequest<GenericAppResult<PostGetVM>>
{
    public int UserId { get; set; }

    public GetAllUserPostsQueryRequest(int userId)
    {
        UserId = userId;
    }
}


public class GetAllUserPostsQueryHandler : IRequestHandler<GetAllUserPostsQueryRequest, GenericAppResult<PostGetVM>>
{
    private readonly IReadRepository<Post> _readRepository;
    private readonly IReadRepository<PostLike> _likeReadRepo;
    private readonly IMapper _mapper;

    public GetAllUserPostsQueryHandler(IReadRepository<Post> readRepository, IMapper mapper, IReadRepository<PostLike> likeReadRepo,IConfiguration configuration)
    {
        _readRepository = readRepository;
        _mapper = mapper;
        _likeReadRepo = likeReadRepo;
   
    }

    public async Task<GenericAppResult<PostGetVM>> Handle(GetAllUserPostsQueryRequest request, CancellationToken cancellationToken)
    {
        var postsList = _readRepository.GetByCondition(p => p.UserId == request.UserId)?.OrderByDescending(p => p.CreationDate).Include(p => p.Comments).ThenInclude(c => c.AppUser).ThenInclude(p=>p.LikedPosts);
        
        if (postsList is null)
        {
            throw new PostException("Post is null from the database.");
        }



        List<PostGetVM> finalList = _mapper.Map<List<PostGetVM>>(postsList);

        return new GenericAppResult<PostGetVM>() { Success = true, Data = finalList };
    }
}
