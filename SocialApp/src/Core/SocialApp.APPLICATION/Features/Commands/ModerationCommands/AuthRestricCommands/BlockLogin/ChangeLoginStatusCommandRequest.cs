using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.AuthRestricCommands.BlockLogin;

public class ChangeLoginStatusCommandRequest : IRequest<AppResult>
{
    public bool IsActive { get; set; }

    public ChangeLoginStatusCommandRequest(bool isActive)
    {
        IsActive = isActive;
    }
}

public class ChangeLoginStatusCommandHandler : IRequestHandler<ChangeLoginStatusCommandRequest, AppResult>
{
    private readonly IWriteRepository<AuthRestriction> _writeRepository;
    private readonly IReadRepository<AuthRestriction> _readRepository;
    public ChangeLoginStatusCommandHandler(IWriteRepository<AuthRestriction> writeRepository, IReadRepository<AuthRestriction> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(ChangeLoginStatusCommandRequest request, CancellationToken cancellationToken)
    {
        var model = _readRepository.Table.FirstOrDefault();

        if (model is null)
        {
            throw new Exception("Not Auth restric model found on database");
        }
        model.LoginActionIsActive = request.IsActive;
        await _writeRepository.UpdateAsync(model);

        return await AppResult.SuccessResult("Login status changed succesfully");
    }
}
