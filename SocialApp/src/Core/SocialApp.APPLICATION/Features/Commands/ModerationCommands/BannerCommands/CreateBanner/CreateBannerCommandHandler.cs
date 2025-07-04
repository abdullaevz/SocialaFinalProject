using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.CreateBanner;

public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommandRequest, AppResult>
{
    private readonly IWriteRepository<Banner> _writeRepository;
    private readonly ICloudUploadService _cloudUploadService;

    public CreateBannerCommandHandler(IWriteRepository<Banner> writeRepository, ICloudUploadService cloudUploadService)
    {
        _writeRepository = writeRepository;
        _cloudUploadService = cloudUploadService;
    }

    public async Task<AppResult> Handle(CreateBannerCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.File is null)
        {
            return await AppResult.Failure("File cannot be null");
        }

        var file = request.File;

        string name = Path.GetFileNameWithoutExtension(file.FileName);
        string extension = Path.GetExtension(file.FileName);
        string newFileName = $"{name}-{Guid.NewGuid().ToString()}-{extension}";


        var result = await _cloudUploadService.UploadPhotoAsync(file, newFileName);

        if (!result.Success)
        {
            return await AppResult.Failure("Something went wrong while proccessing upload photo");
        }

        await _writeRepository.CreateAsync(new Banner() { Url=result.Message });
        return await AppResult.SuccessResult("Banner created");
    }
}
