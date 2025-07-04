using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Shared;

public class GenericAppResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T OneData { get; set; }
    public List<T> Data { get; set; }
    public List<string> Errors { get; set; }

    public static async Task<GenericAppResult<T>> SuccessResult(string message = "Everything is successfull")
    {
        return new() { Success = true, Message = message };
    }

    public static async Task<GenericAppResult<T>> Failure(params string[] errors)
    {
        return new() { Success = false, Message = "An error occured", Errors = errors.ToList() };
    }
}
