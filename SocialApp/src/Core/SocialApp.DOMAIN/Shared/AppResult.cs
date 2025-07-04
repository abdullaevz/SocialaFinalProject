using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Shared;

public class AppResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public bool Response { get; set; }
    public string AIResponse { get; set; }
    public string OnceError { get; set; }
    public AppUser User { get; set; }
    public List<string> Errors { get; set; }

    public static async Task<AppResult> SuccessResult(string message = "Everything is successfull")
    {
        return new() { Success = true, Message = message };
    }

    public static async Task<AppResult> Failure(params string[] errors)
    {
        return new() { Success = false, Message = "An error occured", Errors = errors.ToList() };
    }
}
