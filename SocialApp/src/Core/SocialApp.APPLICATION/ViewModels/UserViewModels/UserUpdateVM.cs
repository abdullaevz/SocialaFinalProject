using Microsoft.AspNetCore.Http;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.ViewModels.UserViewModels
{
    public class UserUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(90, ErrorMessage = "Max 90 chars")]
        public string Fullname { get; set; }
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email adress")]
        public string Email { get; set; }
        public SecurityStatuses Status { get; set; }
        public UserRoles Role{ get; set; }
        public string Country { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsVerified{ get; set; }
        public bool IsDeleted{ get; set; }

        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public string DefaultProfilePhoto { get; set; }
        public IFormFile Image { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string DefaultProfilePhotoPath { get; set; }
        public DateTime DateTime { get; set; }
    
    }
}
