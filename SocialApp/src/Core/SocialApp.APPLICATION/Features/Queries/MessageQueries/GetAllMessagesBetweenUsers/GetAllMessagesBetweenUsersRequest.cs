using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.MessageViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Queries.MessageQueries.GetAllMessagesBetweenUsers;

public class GetAllMessagesBetweenUsersRequest : IRequest<GenericAppResult<MessageGetVM>>
{
    public int SenderId { get; set; }

    public int RecieverId { get; set; }

    public GetAllMessagesBetweenUsersRequest(int senderId, int recieverId)
    {
        SenderId = senderId;
        RecieverId = recieverId;
    }

}

public class GetAllMessagesBetweenUsersHandler : IRequestHandler<GetAllMessagesBetweenUsersRequest, GenericAppResult<MessageGetVM>>
{
    public readonly IReadRepository<Message> _readRepository;

    public GetAllMessagesBetweenUsersHandler(IReadRepository<Message> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<GenericAppResult<MessageGetVM>> Handle(GetAllMessagesBetweenUsersRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new MessageException("Message request is null");
        }

        var messagesQuery = _readRepository.GetByCondition(
            m =>
            (m.SenderId == request.SenderId && m.RecieverId == request.RecieverId) ||
            (m.SenderId == request.RecieverId && m.RecieverId == request.SenderId)
            )?.OrderBy(m => m.CreationDate);

        if (messagesQuery is null)
        {
            return await GenericAppResult<MessageGetVM>.Failure("Messages is null from the database");
        }

        var finalMessagesList = messagesQuery.Select(item => new MessageGetVM()
        {
            SenderId = item.SenderId,
            RecieverId = item.RecieverId,
            Content = item.Content,
            SendDate = item.CreationDate.ToLocalTime().ToString("HH:mm")

        }).ToList();

        return new GenericAppResult<MessageGetVM>() { Success = true, Data = finalMessagesList };
    }
}
