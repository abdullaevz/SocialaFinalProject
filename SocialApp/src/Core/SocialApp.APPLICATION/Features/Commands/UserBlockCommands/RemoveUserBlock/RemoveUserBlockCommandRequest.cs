using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.UserBlockViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.UserBlockCommands.RemoveUserBlock;

public class RemoveUserBlockCommandRequest : IRequest<AppResult>
{
    public CreateBlockVM CreateBlockVM { get; set; }

    public RemoveUserBlockCommandRequest(CreateBlockVM CreateBlockVM)
    {
        this.CreateBlockVM = CreateBlockVM;
    }
}

public class RemoveUserBlockCommandHandler : IRequestHandler<RemoveUserBlockCommandRequest, AppResult>
{
    private readonly IWriteRepository<UserBlock> writeRepository;
    private readonly IReadRepository<UserBlock> readRepository;

    public RemoveUserBlockCommandHandler(IWriteRepository<UserBlock> writeRepository, IReadRepository<UserBlock> readRepository)
    {
        this.writeRepository = writeRepository;
        this.readRepository = readRepository;
    }

    public async Task<AppResult> Handle(RemoveUserBlockCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CreateBlockVM is null)
        {
            throw new Exception("User block request model is null");
        }

        var model = readRepository.GetByCondition(b => b.BlockedBy == request.CreateBlockVM.BlockedBy && b.BlockedUser == request.CreateBlockVM.BlockedId)?.FirstOrDefault();
        if (model is not null)
        {
            await writeRepository.DeleteAsync(model);
            await writeRepository.SaveAsync();
            return await AppResult.SuccessResult();
        }
        return await AppResult.Failure("Model is not found");
    }
}
