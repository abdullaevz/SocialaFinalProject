using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.AuthResctricViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.AuthRestricCommands.UpdateAuthRestrict;

public class UpdateAuthRestricCommandRequest:IRequest<AppResult>
{
    public AuthModeViewModel AuthModeViewModel { get; set; }

    public UpdateAuthRestricCommandRequest(AuthModeViewModel authModeViewModel)
    {
        AuthModeViewModel = authModeViewModel;
    }
}

public class UpdateAuthRestricCommandHandler : IRequestHandler<UpdateAuthRestricCommandRequest, AppResult>
{
    private readonly IReadRepository<AuthRestriction> _readRepository;
    private readonly IWriteRepository<AuthRestriction> _writeRepository;

    public UpdateAuthRestricCommandHandler(IWriteRepository<AuthRestriction> writeRepository, IReadRepository<AuthRestriction> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<AppResult> Handle(UpdateAuthRestricCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.AuthModeViewModel is null)
        {
            throw new Exception("Auth restriction request model is null");
        }

        var model = _readRepository.GetByCondition(m=>m.Id==1)?.FirstOrDefault();
        if (model is not null)
        {
            model.LoginActionIsActive = request.AuthModeViewModel.LoginActionIsActive;
            model.RegisterActionIsActive=request.AuthModeViewModel.RegisterActionIsActive;
            await _writeRepository.SaveAsync();
            return await AppResult.SuccessResult();
        }

        return await AppResult.Failure("This model is null on database");
    }
}
