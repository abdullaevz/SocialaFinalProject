using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.ModerationQueries.AuthRestricQueries.CheckRegister;

public class CheckRegisterQueryRequest : IRequest<GenericAppResult<bool>>
{
}

public class CheckRegisterQueryHandler : IRequestHandler<CheckRegisterQueryRequest, GenericAppResult<bool>>
{
    private readonly IReadRepository<AuthRestriction> _authRestrictionRepository;

    public CheckRegisterQueryHandler(IReadRepository<AuthRestriction> authRestrictionRepository)
    {
        _authRestrictionRepository = authRestrictionRepository;
    }

    public Task<GenericAppResult<bool>> Handle(CheckRegisterQueryRequest request, CancellationToken cancellationToken)
    {
        var model = _authRestrictionRepository.GetByCondition(m=>m.Id==1)?.FirstOrDefault();
        bool result = false;
        if (model is not null)
        {            
            result = model.RegisterActionIsActive;
        }

        return Task.FromResult(new GenericAppResult<bool>() { Success = true, OneData = result });
    }
}