using AutoMapper;
using MediatR;
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

namespace SocialApp.APPLICATION.Features.Commands.MessageCommands.CreateMessage;

public class CreateMessageCommandRequest : IRequest<AppResult>
{
    public CreateMessageVM CreateMessageVM { get; set; }

    public CreateMessageCommandRequest(CreateMessageVM createMessageVM)
    {
        CreateMessageVM = createMessageVM;
    }
}


public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommandRequest, AppResult>
{
    private readonly IWriteRepository<Message> _writeRepository;
    private readonly IMapper _mapper;



    public CreateMessageCommandHandler(IWriteRepository<Message> writeRepository, IMapper mapper)
    {
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<AppResult> Handle(CreateMessageCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.CreateMessageVM is null)
        {
            throw new MessageException("Message create request model is null");
        }

        Message messageModel = _mapper.Map<Message>(request.CreateMessageVM);
        messageModel.CreationDate = DateTime.UtcNow;
        try
        {
            await _writeRepository.CreateAsync(messageModel);

        }
        catch (Exception ex)
        {
            return await AppResult.Failure("Message not created");
        }

        return await AppResult.SuccessResult("Message succesfully saved to database");
    }
}
