using Microsoft.AspNetCore.Http;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.PostViewModels;

public class PostCreateVM
{
    [NotNull]
    [MaxLength(200)]
    public string Content { get; set; }
    public int UserId { get; set; }
    public IFormFile File { get; set; }
}
