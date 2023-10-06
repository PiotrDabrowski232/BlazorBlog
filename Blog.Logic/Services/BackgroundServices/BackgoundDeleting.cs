using Blog.Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blog.Logic.Services.BackgroundServices
{
    public class BackgoundDeleting : BackgroundService
    {
        private ILogger<BackgoundDeleting> _logger;
        private IServiceProvider _serviceProvider;

        public BackgoundDeleting(ILogger<BackgoundDeleting> logger, IServiceProvider serviceProvider)
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
                    
                    _logger.LogInformation("DELETING USER");
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
            }
        }
    }
}
