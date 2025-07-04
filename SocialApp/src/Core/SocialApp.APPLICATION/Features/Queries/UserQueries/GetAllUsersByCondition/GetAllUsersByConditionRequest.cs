using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models.IdentityModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.UserQueries.GetAllUsersByCondition;

public class GetAllUsersByConditionRequest : IRequest<GenericAppResult<UserFilterVM>>
{
    public UserFilterVM UserFilterVM { get; set; }

    public GetAllUsersByConditionRequest(UserFilterVM userFilterVM)
    {
        UserFilterVM = userFilterVM;
    }
}

public class GetAllUsersByConditionHandler : IRequestHandler<GetAllUsersByConditionRequest, GenericAppResult<UserFilterVM>>
{
    private readonly UserManager<AppUser> _userManager;

    public GetAllUsersByConditionHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GenericAppResult<UserFilterVM>> Handle(GetAllUsersByConditionRequest request, CancellationToken cancellationToken)
    {
        if (request.UserFilterVM is null)
        {
            throw new UserException("User request model is null");
        }

        var usersQuery = _userManager.Users.AsQueryable();

        if (request.UserFilterVM.ByNoFilter)
        {
            var rawResult = new UserFilterVM()
            {
                Users = usersQuery.ToList(),
                ByRole = request.UserFilterVM.ByRole,
                ByIsDeleterd = request.UserFilterVM.ByIsDeleterd,
                ByIsPrivite = request.UserFilterVM.ByIsPrivite,
                ByCreationDate = request.UserFilterVM.ByCreationDate,
                ByEmailConfirmed = request.UserFilterVM.ByEmailConfirmed,
                BySecurityStatuses = request.UserFilterVM.BySecurityStatuses,
                ByIsVerified = request.UserFilterVM.ByIsVerified,
            };

            return new GenericAppResult<UserFilterVM>() { Success = true, OneData = rawResult };
        }

        if (request.UserFilterVM.BySecurityStatuses != DOMAIN.Enums.SecurityStatuses.None)
        {
            usersQuery = usersQuery.Where(u => u.SecurityStatus == request.UserFilterVM.BySecurityStatuses);
        }

        if (request.UserFilterVM.ByIsDeleterd)
        {
            usersQuery = usersQuery.Where(u => u.isDeleted == true);
        }
        if (request.UserFilterVM.ByCreationDate.ToString() is not null)
        {
            usersQuery = usersQuery.OrderBy(u => u.CreatedDate);
        }
        if (request.UserFilterVM.ByEmailConfirmed)
        {
            usersQuery = usersQuery.Where(u => u.EmailConfirmed == true);
        }
        if (request.UserFilterVM.ByIsPrivite)
        {
            usersQuery = usersQuery.Where(u => u.IsPrivate == true);
        }
        if (request.UserFilterVM.ByIsVerified)
        {
            usersQuery = usersQuery.Where(u => u.IsVerified == true);
        }
        if (request.UserFilterVM.ByRole!=UserRoles.None)
        {
            usersQuery = usersQuery.Where(u => u.Role==request.UserFilterVM.ByRole);
        }

        var filteredModel = new UserFilterVM()
        {
            Users = usersQuery.ToList(),
            ByRole = request.UserFilterVM.ByRole,
            ByIsDeleterd = request.UserFilterVM.ByIsDeleterd,
            ByIsPrivite = request.UserFilterVM.ByIsPrivite,
            ByCreationDate = request.UserFilterVM.ByCreationDate,
            ByEmailConfirmed = request.UserFilterVM.ByEmailConfirmed,
            BySecurityStatuses = request.UserFilterVM.BySecurityStatuses,
            ByIsVerified = request.UserFilterVM.ByIsVerified,
        };

        return new GenericAppResult<UserFilterVM>() { Success = true, OneData = filteredModel };
    }
}
