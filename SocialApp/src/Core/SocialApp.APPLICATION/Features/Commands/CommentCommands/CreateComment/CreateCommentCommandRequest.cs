using AutoMapper;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.CommentViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.CommentCommands.CreateComment;

public class CreateCommentCommandRequest:IRequest<AppResult>
{
    public CreateCommentVM CommentModel{ get; set; }

    public CreateCommentCommandRequest(CreateCommentVM commentModel)
    {
        CommentModel = commentModel;
    }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest, AppResult>
{
    private readonly IWriteRepository<Comment> _writeRepository;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IWriteRepository<Comment> writeRepository, IMapper mapper)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<AppResult> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CommentModel is null)
        {
            throw new CommentException("Comment request model is null");
        }

        Comment comment = _mapper.Map<Comment>(request.CommentModel);
        comment.CreationDate= DateTime.UtcNow;

        await _writeRepository.CreateAsync(comment);

        return await AppResult.SuccessResult("Created succesfully");
    }
}
