using Microsoft.AspNetCore.Http;
using SocialApp.DOMAIN.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels;

public class AdminUserCreateVM
{

    [Required]
    public string Fullname { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string Description { get; set; }

    public string Country { get; set; }

    public string Profession { get; set; }

    public bool IsPrivate { get; set; } = false;

    public bool IsVerified { get; set; } = false;

    public bool AcceptPolicyStatus { get; set; }

    [Required]
    public UserRoles Role { get; set; }

    [Required]
    public SecurityStatuses SecurityStatus { get; set; }

    public bool IsDeleted { get; set; } = false;

    public IFormFile File { get; set; }
}


