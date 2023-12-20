using Blog.Data.Models;
using Blog.Logic.Dto.UserDtos;
using Blog.Logic.Dto.Validators.UserValidator;
using Blog.Logic.Services;
using Blog.Logic.Services.BackgroundServices;
using Blog.Logic.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Blog.Configuration
{
    public static class ServicesDependencyConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IPostService, PostService>();
            Services.AddScoped<ITagService, TagService>();
            Services.AddScoped<ITagPostsService, TagPostsService>();
            Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
            Services.AddHostedService<BackgroundDeleting>();

            //Azure 
            Services.AddScoped<IKeyVaultService, KeyVaultService>();
            Services.AddScoped<IAzureQueueMessageSender, AzureQueueMessageSender>();

            return Services;

        }
    }
}
