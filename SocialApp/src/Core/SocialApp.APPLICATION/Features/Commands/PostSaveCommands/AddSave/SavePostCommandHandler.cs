using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.PostSaveCommands.AddSave;

public class SavePostCommandHandler : IRequestHandler<SavePostCommandRequest, AppResult>
{
    private readonly IWriteRepository<PostSave> _saveWriteRepository;
    private readonly IReadRepository<PostSave> _readRepository;

    public SavePostCommandHandler(IWriteRepository<PostSave> saveWriteRepository, IReadRepository<PostSave> readRepository)
    {
        _saveWriteRepository = saveWriteRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(SavePostCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CreatePostSaveVM is null)
        {
            throw new PostException("Post save request model is null");
        }

        var saveModel = _readRepository.GetByCondition(m=>m.UserId==request.CreatePostSaveVM.UserId&&m.PostId==request.CreatePostSaveVM.PostId)?.FirstOrDefault();

        if (saveModel is not null)
        {
            return await AppResult.Failure("This post is already saved");
        }

        PostSave postSave = new() { UserId = request.CreatePostSaveVM.UserId, PostId = request.CreatePostSaveVM.PostId };
        await _saveWriteRepository.CreateAsync(postSave);
        return await AppResult.SuccessResult("Post saved succesfully");
    }
}
