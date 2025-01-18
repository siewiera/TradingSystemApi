using TradingSystemApi.Interface.ServicesInterface;

namespace TradingSystemApi.Services.StartupTask
{
    public class StartupTask : IStartupFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            { 
                var initService = scope.ServiceProvider.GetService<IInitService>();
                //if(initService != null)
                    initService.CheckInitDataExists();
            }
            return next;
        }
    }
}
