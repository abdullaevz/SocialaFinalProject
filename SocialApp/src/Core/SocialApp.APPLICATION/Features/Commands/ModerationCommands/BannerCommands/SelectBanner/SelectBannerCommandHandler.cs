using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;

namespace SocialApp.APPLICATION.Features.Commands.ModerationCommands.BannerCommands.SelectBanner;

public class SelectBannerCommandHandler : IRequestHandler<SelectBannerCommandRequest, AppResult>
{
    private readonly IWriteRepository<Banner> _writeRepository;
    private readonly IReadRepository<Banner> _readRepository;

    public SelectBannerCommandHandler(IReadRepository<Banner> readRepository, IWriteRepository<Banner> writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<AppResult> Handle(SelectBannerCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.SelectBannerVM is null)
        {
            throw new Exception("Banner request model is null");
        }
        var allBanners = _readRepository.GetByCondition(m => m.IsActive == true);
        if (allBanners is not null)
        {
            foreach (var item in allBanners)
            {
                item.IsActive = false;
            }
        }

        var bannerModel = await _readRepository.GetByIdAsync(request.SelectBannerVM.BannerId);
        if (bannerModel is null)
        {
            return await AppResult.Failure("This model not fount on database");
        }
        bannerModel.IsActive = true;
        await _writeRepository.SaveAsync();
        return await AppResult.SuccessResult("Banner selected succesfully");

    }
}
