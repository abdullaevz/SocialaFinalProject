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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.PostQueries.GetAllPostsByCondition;

public class GetAllPostsByConditionRequest : IRequest<GenericAppResult<PostFilterVM>>
{
    public PostFilterVM PostFilterVM { get; set; }

    public GetAllPostsByConditionRequest(PostFilterVM postFilterVM)
    {
        PostFilterVM = postFilterVM;
    }
}

public class GetAllPostsByConditionHandler : IRequestHandler<GetAllPostsByConditionRequest, GenericAppResult<PostFilterVM>>
{
    private readonly IReadRepository<Post> _readRepository;
    private readonly IMapper _mapper;

    public GetAllPostsByConditionHandler(IReadRepository<Post> readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<GenericAppResult<PostFilterVM>> Handle(GetAllPostsByConditionRequest request, CancellationToken cancellationToken)
    {
        if (request.PostFilterVM is null)
        {
            throw new PostException("Post request model is null");
        }

        var postQuery = _readRepository.GetAll();

        if (request.PostFilterVM.ByNoFilter)
        {
            postQuery = postQuery.Include(p => p.User).ThenInclude(p => p.Comments);
            List<PostGetVM> rawPostGetVMs = _mapper.Map<List<PostGetVM>>(postQuery);
            var rawModel = new PostFilterVM()
            {
                Posts = rawPostGetVMs,
                ByIsDeleted = request.PostFilterVM.ByIsDeleted,
                ByCreationDate = request.PostFilterVM.ByCreationDate,
                BySecurityStatuses = request.PostFilterVM.BySecurityStatuses,
                ByLikeCount = request.PostFilterVM.ByLikeCount,
            };
            return new GenericAppResult<PostFilterVM>() { Success = true, OneData = rawModel };
        }

        if (request.PostFilterVM.ByIsDeleted)
        {
            postQuery = postQuery.Where(p => p.IsDeleted == true);
        }

        if (request.PostFilterVM.ByCreationDate)
        {
            postQuery = postQuery.OrderByDescending(p => p.CreationDate);
        }

        if (request.PostFilterVM.ByLikeCount)
        {
            postQuery = postQuery.OrderByDescending(p => p.LikeCount);
        }

        if (request.PostFilterVM.BySecurityStatuses != DOMAIN.Enums.SecurityStatuses.None)
        {
            postQuery = postQuery.Where(p => p.SecurityStatus == request.PostFilterVM.BySecurityStatuses);
        }

        if (request.PostFilterVM.ByUser != 0)
        {
            postQuery = postQuery.Where(p => p.UserId == request.PostFilterVM.ByUser);
        }

        postQuery = postQuery.Include(p => p.User).ThenInclude(p=>p.Comments);

        List<PostGetVM> postGetVMs = _mapper.Map<List<PostGetVM>>(postQuery);

        var filteredModel = new PostFilterVM()
        {
            Posts = postGetVMs,
            ByIsDeleted = request.PostFilterVM.ByIsDeleted,
            ByCreationDate = request.PostFilterVM.ByCreationDate,
            BySecurityStatuses = request.PostFilterVM.BySecurityStatuses,
            ByLikeCount = request.PostFilterVM.ByLikeCount,
        };
        return new GenericAppResult<PostFilterVM>() { Success = true, OneData = filteredModel };

    }
}
