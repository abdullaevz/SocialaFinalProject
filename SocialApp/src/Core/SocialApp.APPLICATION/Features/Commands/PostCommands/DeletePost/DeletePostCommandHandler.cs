using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.PostCommands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, AppResult>
{
    private readonly IWriteRepository<Post> _postRepository;
    private readonly IReadRepository<Post> _readRepository;

    public DeletePostCommandHandler(IWriteRepository<Post> postRepository, IReadRepository<Post> readRepository)
    {
        _postRepository = postRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
    {
        var post = await _readRepository.GetByIdAsync(request.Id);
        if (post is null)
        {
            return await AppResult.Failure("Cannot find any model with this id");
        }
        await _postRepository.SoftDeleteAsync(post);
        await _postRepository.SaveAsync();
        //Delete codes
        return await AppResult.SuccessResult("Deleted successfully") ;
    }
}
