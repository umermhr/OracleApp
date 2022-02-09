namespace App.Oracle.Core.Worker.Service
{
    public class BackgroundTaskWorker : BackgroundService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;

        public BackgroundTaskWorker(IConfiguration configuration, IServiceProvider provider)
        {
            _configuration = configuration;
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await using (var scope = _provider.CreateAsyncScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IBackgroundTaskEngine>();
                    await service.StartEngine();
                }

                await Task.Delay(TimeSpan.FromSeconds(double.Parse(_configuration["Worker:DelayTimeInSeconds"])), stoppingToken);
            }
        }
    }
}