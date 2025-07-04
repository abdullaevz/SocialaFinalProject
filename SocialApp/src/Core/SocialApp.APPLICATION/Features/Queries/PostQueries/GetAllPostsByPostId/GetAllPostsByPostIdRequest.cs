using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

namespace SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPostsByPostId;

public class GetAllPostsByPostIdRequest : IRequest<GenericAppResult<PostGetVM>>
{
    public int PostId { get; set; }

    public GetAllPostsByPostIdRequest(int postId)
    {
        PostId = postId;
    }
}

public class GetAllPostsByPostIdHandler : IRequestHandler<GetAllPostsByPostIdRequest, GenericAppResult<PostGetVM>>
{

    private readonly IReadRepository<Post> _readRepository;
    private readonly IMapper _mapper;

    public GetAllPostsByPostIdHandler(IReadRepository<Post> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<PostGetVM>> Handle(GetAllPostsByPostIdRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new PostException("Post request model is null");
        }

        var post = _readRepository.GetByCondition(p=>p.Id==request.PostId)?.Include(p=>p.User).Include(p=>p.Comments).ThenInclude(c=>c.AppUser).FirstOrDefault();

        if (post is null)
        {
            return await GenericAppResult<PostGetVM>.Failure($"Post model not found on database with this id {request.PostId}");
        }


        PostGetVM postGetVM = _mapper.Map<PostGetVM>(post);

        return new GenericAppResult<PostGetVM>() { Success=true,OneData=postGetVM };
        
    }
}
