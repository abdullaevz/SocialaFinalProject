using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Features.Queries.PostSaveQueries.GetSavedPostsByUserId;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostLikeQueries.GetLikedPostsByUserId;

public class GetLikedPostsByUserIdRequest:IRequest<GenericAppResult<PostGetVM>>
{
    public int UserId { get; set; }

    public GetLikedPostsByUserIdRequest(int userId)
    {
        UserId = userId;
    }
}

public class GetLikedPostsByUserIdHandler : IRequestHandler<GetLikedPostsByUserIdRequest, GenericAppResult<PostGetVM>>
{
    private readonly IReadRepository<PostLike> _readRepository;
    private readonly IMapper _mapper;

    public GetLikedPostsByUserIdHandler(IReadRepository<PostLike> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<PostGetVM>> Handle(GetLikedPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new PostException("Post like request model is null");
        }

        var likedPosts = _readRepository.GetByCondition(p => p.UserId == request.UserId)?.Include(p=>p.Post).ThenInclude(p=>p.User).Select(p=>p.Post)?.ToList();
        if (likedPosts is null)
        {
            return await GenericAppResult<PostGetVM>.Failure("List is null from the database");
        }

     

        List<PostGetVM> finalList = _mapper.Map<List<PostGetVM>>(likedPosts);

        return new GenericAppResult<PostGetVM>() { Success = true, Data = finalList };
    }
}