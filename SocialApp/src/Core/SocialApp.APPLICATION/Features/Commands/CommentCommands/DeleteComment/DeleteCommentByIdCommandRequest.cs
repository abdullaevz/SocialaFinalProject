using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.CommentCommands.DeleteComment;

public class DeleteCommentByIdCommandRequest : IRequest<AppResult>
{
    public int CommentId { get; set; }

    public DeleteCommentByIdCommandRequest(int commentId)
    {
        CommentId = commentId;
    }
}

public class DeleteCommentByIdCommandHandler : IRequestHandler<DeleteCommentByIdCommandRequest, AppResult>
{
    private readonly IWriteRepository<Comment> _writeRepository;
    private readonly IReadRepository<Comment> _readRepository;

    public DeleteCommentByIdCommandHandler(IWriteRepository<Comment> writeRepository, IReadRepository<Comment> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(DeleteCommentByIdCommandRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new CommentException("Comment request model is null");
        }

        var model = await _readRepository.GetByIdAsync(request.CommentId);

        if (model is null)
        {
            return await AppResult.Failure($"Cannot find any model with this id{request.CommentId}");
        }

        await _writeRepository.SoftDeleteAsync(model);

        return await AppResult.SuccessResult($"Comment model is deleted succesfully with this id {request.CommentId}");
    }
}
