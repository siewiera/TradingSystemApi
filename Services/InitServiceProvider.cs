
using TradingSystemApi.Interface.ServicesInterface;

namespace TradingSystemApi.Services
{
    public class InitServiceProvider : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public InitServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            { 
                var initService = scope.ServiceProvider.GetService<IInitService>();
                if (initService != null)
                    await initService.CheckInitDataExists();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
