using AutoMapper;
using SocialApp.APPLICATION.ViewModels.CommentViewModels;
using SocialApp.APPLICATION.ViewModels.MessageViewModels;
using SocialApp.APPLICATION.ViewModels.PostLikeViewModels;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.APPLICATION.ViewModels.PostViewModels;
using SocialApp.APPLICATION.ViewModels.SocialMediaViewModels;
using SocialApp.APPLICATION.ViewModels.UserViewModels;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PostCreateVM, Post>().ReverseMap();
        CreateMap<Post, PostGetVM>().ReverseMap();
        CreateMap<Post, PostUpdateVM>().ReverseMap();
        CreateMap<PostGetVM, PostUpdateVM>().ReverseMap();
        CreateMap<SocialAccountCreateAndUpdateVM,SocialAccount>().ReverseMap();
        CreateMap<CreateCommentVM,Comment>().ReverseMap();
        CreateMap<GetComment, Comment>().ReverseMap();
        CreateMap<PostLike, LikesVM>().ReverseMap();
        CreateMap<PostSave, CreateUpdatePostSaveVM>().ReverseMap();
        CreateMap<PostSave, SaveGet>().ReverseMap();
        CreateMap<Message, CreateMessageVM>().ReverseMap();
    }
}
