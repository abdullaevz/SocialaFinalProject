using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.PostLikeCommands.RemoveLike;

public class RemoveLikeFromPostCommandHandler : IRequestHandler<RemoveLikeFromPostCommandRequest, AppResult>
{
    private readonly IWriteRepository<Post> _postWriteRepository;
    private readonly IWriteRepository<PostLike> _postLikeWriteRepository;
    private readonly IReadRepository<PostLike> _postLikeReadRepository;
    private readonly IReadRepository<Post> _postReadRepository;

    public RemoveLikeFromPostCommandHandler(IWriteRepository<Post> postWriteRepository, IWriteRepository<PostLike> postLikeWriteRepository, IReadRepository<PostLike> postLikeReadRepository, IReadRepository<Post> postReadRepository)
    {
        _postWriteRepository = postWriteRepository;
        _postLikeWriteRepository = postLikeWriteRepository;
        _postLikeReadRepository = postLikeReadRepository;
        _postReadRepository = postReadRepository;
    }


    public async Task<AppResult> Handle(RemoveLikeFromPostCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.LikeCreateVM is null)
        {
            throw new PostLikeException("Like request model is null");
        }

        var postId = request.LikeCreateVM.PostId;
        var userId = request.LikeCreateVM.UserId;

        // İlgili Like modelini bul
        var like = _postLikeReadRepository
            .GetByCondition(x => x.PostId == postId && x.UserId == userId)
            ?.FirstOrDefault();

        if (like != null)
        {
            // Like varsa sil
            await _postLikeWriteRepository.DeleteAsync(like);
            // Post'u bul
            var post = _postReadRepository.GetByCondition(p => p.Id == postId)?.FirstOrDefault();
            if (post != null && post.LikeCount > 0)
            {
                post.LikeCount -= 1;
                await _postWriteRepository.UpdateAsync(post);
            }
            await _postLikeWriteRepository.SaveAsync();

            return await AppResult.SuccessResult("Beğeni kaldırıldı.");
        }


        return await AppResult.SuccessResult("Post unliked succesfully");
    }
}
