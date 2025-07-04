using Microsoft.AspNetCore.Http;
using SocialApp.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.BannerViewModels;

public class BannerVM
{
    public List<Banner> BannerList { get; set; }
    public IFormFile File { get; set; }
}
