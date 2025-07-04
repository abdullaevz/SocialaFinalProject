using AutoMapper.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.APPLICATION.Cloudinary;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.INFRASTRUCTURE.Concretes.Servcies;

public class CloudUploadService : ICloudUploadService
{
    private readonly ICloudinary _cloudinary;

    public CloudUploadService(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        var settings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<AppResult> UploadPhotoAsync(IFormFile file, string name)
    {
        var publicId = Path.GetFileNameWithoutExtension(name);

        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(name, stream),
            PublicId = publicId,
            Overwrite = true,
            Folder = "Images"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return await AppResult.Failure("Upload exception occured");
        }

        return new AppResult() { Success = true, Message = result.SecureUrl.ToString() };
        
    }

    public async Task<AppResult> UploadVideoAsync(IFormFile file, string name)
    {
        var publicId = Path.GetFileNameWithoutExtension(name);

        using var stream = file.OpenReadStream();

        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(name, stream),
            PublicId = publicId,
            Overwrite = true,
            Folder = "Videos"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return await AppResult.Failure("Video upload failed");
        }

        return new AppResult() { Success = true, Message = result.SecureUrl.ToString() };
    }

}
