
using System.Threading;
using TradingSystemApi.Context;
using TradingSystemApi.Interface.ServicesInterface;

namespace TradingSystemApi.Services.ScheduleService
{
    public class ScheduleService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduleService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = 60;
                using (var scope = _serviceProvider.CreateScope())
                {
                    var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
                    await sessionService.RemoveInactiveSession();
                    delay = sessionService.LoadingSessionSettingsFile().delay;
                }

                await Task.Delay(TimeSpan.FromSeconds(delay), stoppingToken);
            }
        }
    }
}
