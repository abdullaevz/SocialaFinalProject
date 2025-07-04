using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPosts;

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQueryRequest, GenericAppResult<PostGetVM>>
{
    private readonly IReadRepository<Post> _readRepository;
    private readonly IMapper _mapper;
    public GetAllPostsQueryHandler(IReadRepository<Post> readRepository, IMapper mapper, UserManager<AppUser> userManager)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    async Task<GenericAppResult<PostGetVM>> IRequestHandler<GetAllPostsQueryRequest, GenericAppResult<PostGetVM>>.Handle(GetAllPostsQueryRequest request, CancellationToken cancellationToken)
    {
        var existPosts = _readRepository.GetAll().OrderByDescending(p => p.CreationDate).OrderByDescending(p => p.User.IsVerified == true)
            .Where(p => p.SecurityStatus == SecurityStatuses.SAFE && p.IsDeleted == false)
            .Include(p => p.User).ToList();
        var posts = existPosts.Select(item => _mapper.Map<PostGetVM>(item)).ToList();

        return new GenericAppResult<PostGetVM>
        {
            Success = true,
            Data = posts,
        };

    }
}
