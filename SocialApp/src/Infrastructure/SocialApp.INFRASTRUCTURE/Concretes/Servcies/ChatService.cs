using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialApp.APPLICATION.Features.Commands.MessageCommands.CreateMessage;
using SocialApp.APPLICATION.ViewModels.MessageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.INFRASTRUCTURE.Concretes.Servcies;

public class ChatService : Hub
{
    private readonly IMediator _mediator;
    public static readonly Dictionary<string, string> OnlineUsers = new();

    public ChatService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(string receiverId, string message)
    {

        var senderId = Context.UserIdentifier;
        if (!int.TryParse(senderId, out int senderIdInt))
        {
            return;
        }

        if (!int.TryParse(receiverId, out int recieverIdInt))
        {
            return;
        }
        var sentAt = DateTime.Now.ToString("HH:mm");
        await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, receiverId, message, sentAt);

        var result = await _mediator.Send(new CreateMessageCommandRequest(new CreateMessageVM()
        {
            SenderId = senderIdInt,
            RecieverId = recieverIdInt,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false,
            Content = message
        }));

        Console.WriteLine(result.Message);

    }

    public override Task OnConnectedAsync()
    {

        string userId = Context.UserIdentifier;

        if (!string.IsNullOrEmpty(userId))
        {
            OnlineUsers[userId] = userId;
        }

        Console.WriteLine("User connected " + userId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string userId = Context.UserIdentifier;

        if (!string.IsNullOrEmpty(userId))
        {
            OnlineUsers.Remove(userId);
        }

        Console.WriteLine("User Disconnected " + userId);
        return base.OnDisconnectedAsync(exception);
    }

    public static bool UserIsOnline(string userId)
    {
        return OnlineUsers.ContainsKey(userId);
    }
}
