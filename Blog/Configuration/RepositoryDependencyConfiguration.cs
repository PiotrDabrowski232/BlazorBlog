using Blog.Data.Repositories.Interfaces;
using Blog.Data.Repositories;

namespace Blog.Configuration
{
    public static class RepositoryDependencyConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IRoleRepository, RoleReposiotry>();
            Services.AddScoped<IPostRepository, PostRepository>();
            Services.AddScoped<ITagRepository, TagRepository>();
            Services.AddScoped<ITagPostsRepository, TagPostsRepository>();

            return Services;
        }
    }
}
