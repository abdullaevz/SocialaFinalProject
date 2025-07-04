using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostCommands.UpdatePost;

public class UpdatePostCommandRequest:IRequest<AppResult>
{
    public PostUpdateVM PostUpdateVM { get; set; }

    public UpdatePostCommandRequest(PostUpdateVM postUpdateVM)
    {
        PostUpdateVM = postUpdateVM;
    }
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, AppResult>
{
    private readonly IReadRepository<Post> readRepository;
    private readonly IWriteRepository<Post> writeRepository;
    public UpdatePostCommandHandler(IReadRepository<Post> readRepository, IWriteRepository<Post> writeRepository)
    {
        this.readRepository = readRepository;
        this.writeRepository = writeRepository;
    }

    public async Task<AppResult> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.PostUpdateVM is null)
        {
            throw new PostException("Post request model is null");
        }

        var postModel = await readRepository.GetByIdAsync(request.PostUpdateVM.PostId);
        if (postModel is not null)
        {
            postModel.SecurityStatus=request.PostUpdateVM.Status;
            await writeRepository.SaveAsync();
        }

        return await AppResult.SuccessResult();
    }
}
