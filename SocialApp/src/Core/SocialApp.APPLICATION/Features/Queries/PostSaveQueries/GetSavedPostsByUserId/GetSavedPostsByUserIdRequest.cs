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

namespace SocialApp.APPLICATION.Features.Queries.PostSaveQueries.GetSavedPostsByUserId;

public class GetSavedPostsByUserIdRequest : IRequest<GenericAppResult<PostGetVM>>
{
    public int UserId { get; set; }

    public GetSavedPostsByUserIdRequest(int userId)
    {
        UserId = userId;
    }
}

public class GetSavedPostsByUserIdHandler : IRequestHandler<GetSavedPostsByUserIdRequest, GenericAppResult<PostGetVM>>
{
    private readonly IReadRepository<PostSave> _readRepository;
    private readonly IMapper _mapper;

    public GetSavedPostsByUserIdHandler(IReadRepository<PostSave> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<PostGetVM>> Handle(GetSavedPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new PostException("Post save request model is null");
        }

        var savedPosts = _readRepository.GetByCondition(p => p.UserId == request.UserId)?.Include(p => p.Post).ThenInclude(p=>p.User).Select(p => p.Post).ToList();
        if (savedPosts is null)
        {
            return await GenericAppResult<PostGetVM>.Failure("List is null from the database");
        }

        List<PostGetVM> finalList = _mapper.Map<List<PostGetVM>>(savedPosts);

        return new GenericAppResult<PostGetVM>() { Success = true, Data = finalList };
    }
}
