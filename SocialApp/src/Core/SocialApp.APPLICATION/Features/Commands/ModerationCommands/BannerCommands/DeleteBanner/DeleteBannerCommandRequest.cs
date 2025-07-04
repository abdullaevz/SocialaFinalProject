using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.DeleteBanner;

public class DeleteBannerCommandRequest:IRequest<AppResult>
{
    public int Id { get; set; }

    public DeleteBannerCommandRequest(int ıd)
    {
        Id = ıd;
    }
}

public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommandRequest, AppResult>
{
    private readonly IWriteRepository<Banner> _writeRepository;
    private readonly IReadRepository<Banner> _readRepository;


    public DeleteBannerCommandHandler(IWriteRepository<Banner> writeRepository, IReadRepository<Banner> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }


    public async Task<AppResult> Handle(DeleteBannerCommandRequest request, CancellationToken cancellationToken)
    {
        var model = await _readRepository.GetByIdAsync(request.Id);
        if (model is not null)
        {
            await _writeRepository.DeleteAsync(model);
        }

        return await AppResult.SuccessResult("Banner deleted succesfully");
    }
}
