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

namespace SocialApp.APPLICATION.Features.Commands.UserBlockCommands.CreateUserBlock;

public class AddBlockCommandRequest:IRequest<AppResult>
{
    public CreateBlockVM CreateBlockVM { get; set; }

    public AddBlockCommandRequest(CreateBlockVM createBlockVM)
    {
        CreateBlockVM = createBlockVM;
    }
}

public class AddBlockCommandHandler : IRequestHandler<AddBlockCommandRequest, AppResult>
{
    private readonly IWriteRepository<UserBlock> writeRepository;
    private readonly IReadRepository<UserBlock> readRepository;

    public AddBlockCommandHandler(IWriteRepository<UserBlock> writeRepository, IReadRepository<UserBlock> readRepository)
    {
        this.writeRepository = writeRepository;
        this.readRepository = readRepository;
    }

    public async Task<AppResult> Handle(AddBlockCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CreateBlockVM is null)
        {
            throw new Exception("Create Block model is null");
        }

        var model = readRepository.GetByCondition(b => b.BlockedUser == request.CreateBlockVM.BlockedId)?.FirstOrDefault(); ;
        if (model is null)
        {
            await writeRepository.CreateAsync(new UserBlock() { BlockedBy=request.CreateBlockVM.BlockedBy,BlockedUser=request.CreateBlockVM.BlockedId });
        }

        return await AppResult.SuccessResult();
    }
}
