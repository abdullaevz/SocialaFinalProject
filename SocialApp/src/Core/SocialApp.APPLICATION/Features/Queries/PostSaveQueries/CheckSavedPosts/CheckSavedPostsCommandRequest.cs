using AutoMapper;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostSaveQueries.CheckSavedPosts;

public class CheckSavedPostsCommandRequest:IRequest<GenericAppResult<SaveGet>>
{
    public int UserId { get; set; }

    public CheckSavedPostsCommandRequest(int userId)
    {
        UserId = userId;
    }
}

public class CheckSavedPostsCommandHandler : IRequestHandler<CheckSavedPostsCommandRequest, GenericAppResult<SaveGet>>
{
    private readonly IReadRepository<PostSave> _readRepository;
    private readonly IMapper _mapper;

    public CheckSavedPostsCommandHandler(IReadRepository<PostSave> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<SaveGet>> Handle(CheckSavedPostsCommandRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new PostException("Post save request model is null");
        }

        var savedPostsQueyList = _readRepository.GetByCondition(m=>m.UserId==request.UserId);
        
        if (savedPostsQueyList is null)
        {
            return await GenericAppResult<SaveGet>.Failure("Cannot find any saved post from the database");
        }

        List<SaveGet> savedPosts = _mapper.Map<List<SaveGet>>(savedPostsQueyList.ToList());
        return new GenericAppResult<SaveGet>() { Success = true, Data = savedPosts };
    }
}
