using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostSaveCommands.RemoveSave;

public class RemoveSaveFromPostCommandRequest:IRequest<AppResult>
{
    public CreateUpdatePostSaveVM CreateUpdatePostSaveVM { get; set; }

    public RemoveSaveFromPostCommandRequest(CreateUpdatePostSaveVM createUpdatePostSaveVM)
    {
        CreateUpdatePostSaveVM = createUpdatePostSaveVM;
    }
}

public class RemoveSaveFromPostCommandHandler : IRequestHandler<RemoveSaveFromPostCommandRequest, AppResult>
{
    private readonly IWriteRepository<PostSave> _writeRepository;
    private readonly IReadRepository<PostSave> _readRepository;

    public RemoveSaveFromPostCommandHandler(IWriteRepository<PostSave> writeRepository, IReadRepository<PostSave> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(RemoveSaveFromPostCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CreateUpdatePostSaveVM is null)
        {
            throw new PostException("Post remove save model is null");
        }

        var saveModel = _readRepository.GetByCondition(m => m.UserId == request.CreateUpdatePostSaveVM.UserId && m.PostId == request.CreateUpdatePostSaveVM.PostId)?.FirstOrDefault();

        if (saveModel is not null)
        {
            await _writeRepository.DeleteAsync(saveModel);
            await _writeRepository.SaveAsync();
            return await AppResult.SuccessResult("Saved status succesfully removed from the post");
        }


        return await AppResult.Failure("This post already unsaved");

    }
}
