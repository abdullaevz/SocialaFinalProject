using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckLogin;

public class CheckLoginQueryRequest:IRequest<GenericAppResult<bool>>
{
}

public class CheckLoginQueryHandler : IRequestHandler<CheckLoginQueryRequest, GenericAppResult<bool>>
{
    private readonly IReadRepository<AuthRestriction> _authRestrictionRepository;

    public CheckLoginQueryHandler(IReadRepository<AuthRestriction> authRestrictionRepository)
    {
        _authRestrictionRepository = authRestrictionRepository;
    }

    public Task<GenericAppResult<bool>> Handle(CheckLoginQueryRequest request, CancellationToken cancellationToken)
    {
        var model = _authRestrictionRepository.GetByCondition(m => m.Id == 1)?.FirstOrDefault();
       
        bool result = false;
        if (model is not null)
        {
            result = model.LoginActionIsActive;
        }

        return Task.FromResult(new GenericAppResult<bool>() { Success = true, OneData = result });
    }
}
