using AutoMapper;
using CloudinaryDotNet.Actions;
using MediatR;
using SocialApp.APPLICATION.Abstractions.Repositories;
using SocialApp.APPLICATION.Abstractions.Services;
using SocialApp.DOMAIN.Exceptions;
using SocialApp.DOMAIN.Models;
using SocialApp.DOMAIN.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.APPLICATION.Features.Commands.PostCommands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, AppResult>
{
    private readonly IWriteRepository<Post> _postRepo;
    private readonly IMapper _mapper;
    private readonly ICloudUploadService _cloudUploadService;
    public CreatePostCommandHandler(IWriteRepository<Post> postRepo, IMapper mapper, ICloudUploadService cloudUploadService)
    {
        _postRepo = postRepo;
        _mapper = mapper;
        _cloudUploadService = cloudUploadService;
    }

    public async Task<AppResult> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
    {
        if (request.PostCreateVM is null || request.PostCreateVM.Content.Equals(0))
        {
            throw new PostException("Post vm model null or content contains nothing");
        }

        //AI VALIDATION


        //Mapping
        Post post = _mapper.Map<Post>(request.PostCreateVM);
        post.CreationDate = DateTime.UtcNow;
        post.SecurityStatus = DOMAIN.Enums.SecurityStatuses.SAFE;

        //File save procces
        if (request.PostCreateVM.File is not null)
        {
            var file = request.PostCreateVM.File;

            string name = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string newFileName = $"{name}-{Guid.NewGuid().ToString()}-{extension}";

            var result = file.ContentType.Contains("image") ? await _cloudUploadService.UploadPhotoAsync(file, newFileName) : await _cloudUploadService.UploadVideoAsync(file, newFileName);


            if (!result.Success)
            {
                return await AppResult.Failure("Something went wrong while proccessing upload photo");
            }

            post.MediaPath = result.Message;

        }
        //Database save proccess
        await _postRepo.CreateAsync(post);

        return await AppResult.SuccessResult();
    }
}
