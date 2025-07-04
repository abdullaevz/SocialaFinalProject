using MediatR;
using SocialApp.APPLICATION.ViewModels.PostSaveViewModels;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostSaveCommands.AddSave;

public class SavePostCommandRequest : IRequest<AppResult>
{
    public CreateUpdatePostSaveVM CreatePostSaveVM { get; set; }

    public SavePostCommandRequest(CreateUpdatePostSaveVM createPostSaveVM)
    {
        CreatePostSaveVM = createPostSaveVM;
    }
}
