using Blog.Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Logic.Services.BackgroundServices
{
    public class BackgroundDeleting : BackgroundService
    {
        private ILogger<BackgroundDeleting> _logger;
        private IServiceProvider _serviceProvider;

        public BackgroundDeleting(ILogger<BackgroundDeleting> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetService<IUserService>();
                    if (!scopedService.CheckUsersToDelete().IsNullOrEmpty())
                    {
                        _logger.LogInformation("Deleted Users");
                    }
                    await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
                }
            }
        }
    }
}
