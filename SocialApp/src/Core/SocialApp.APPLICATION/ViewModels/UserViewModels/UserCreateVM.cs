using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class UserCreateVM
{
    [NotNull]
    [MaxLength(90, ErrorMessage = "Maksimum 90 simvol daxil edilə bilər.")]
    public string Fullname { get; set; }
    [NotNull]
    [MaxLength(20, ErrorMessage = "Maksimum 20 simvol daxil edilə bilər.")]
    public string Username { get; set; }
    [NotNull]
    [EmailAddress(ErrorMessage ="Email adresi yalnışdır.")]
    public string Email{ get; set; }
    [NotNull]
    [MinLength(5,ErrorMessage ="Minimum 5 simvol olmalıdır")]
    public string Password{ get; set; }
    [Required(ErrorMessage = "You need to Accept Term and Conditions")]
    public bool Policy { get; set; }
}
