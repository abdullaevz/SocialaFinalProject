using Microsoft.AspNetCore.Identity;
using SocialApp.DOMAIN.Enums;
using SocialApp.DOMAIN.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DOMAIN.Models.IdentityModels;

public class AppUser : IdentityUser<int>
{
    public string Fullname { get; set; }
    public string ProfilePhotoPath { get; set; }
    public string DefaultProfilePath { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public string Profession { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsVerified { get; set; }
    public bool AcceptPolicyStatus { get; set; }
    public UserRoles Role { get; set; }
    public SecurityStatuses SecurityStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool isDeleted { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<SocialAccount> SocialAccounts { get; set; }
    public List<Story> Stories { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<PostLike> LikedPosts { get; set; }
    public ICollection<PostSave> SavedPosts { get; set; }
    
}
