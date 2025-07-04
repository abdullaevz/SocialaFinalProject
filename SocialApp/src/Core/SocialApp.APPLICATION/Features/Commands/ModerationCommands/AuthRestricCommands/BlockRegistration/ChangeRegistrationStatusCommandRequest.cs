using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.AuthRestricCommands.BlockRegistration;

public class ChangeRegistrationStatusCommandRequest : IRequest<AppResult>
{
    public bool IsActive { get; set; }

    public ChangeRegistrationStatusCommandRequest(bool isActive)
    {
        IsActive = isActive;
    }
}

public class ChangeRegistrationStatusCommandHandler : IRequestHandler<ChangeRegistrationStatusCommandRequest, AppResult>
{
    private readonly IWriteRepository<AuthRestriction> _writeRepository;
    private readonly IReadRepository<AuthRestriction> _readRepository;
    public ChangeRegistrationStatusCommandHandler(IWriteRepository<AuthRestriction> writeRepository, IReadRepository<AuthRestriction> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(ChangeRegistrationStatusCommandRequest request, CancellationToken cancellationToken)
    {
        var model = _readRepository.Table.FirstOrDefault();

        if (model is null)
        {
            throw new Exception("Not Auth restric model found on database");
        }
        model.RegisterActionIsActive=request.IsActive;
        await _writeRepository.UpdateAsync(model);

        return await AppResult.SuccessResult("Registration status changed succesfully");
    }
}
