using Microsoft.AspNetCore.Http;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Abstractions.Services;

public interface ICloudUploadService
{
    Task<AppResult> UploadPhotoAsync(IFormFile file, string name);
    Task<AppResult> UploadVideoAsync(IFormFile file,string name);
}
