namespace App.Oracle.Core.Worker.Service.Repository
{
    public class BackgroundTaskEngine : IBackgroundTaskEngine
    {
        private readonly IConfiguration _configuration;
        private readonly IBackgroundTasks _backgroundTasks;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BackgroundTaskEngine(IConfiguration configuration, IBackgroundTasks backgroundTasks)
        {
            _configuration = configuration;
            _backgroundTasks = backgroundTasks;
        }

        public async Task StartEngine()
        {
            try
            {
                var statusCode = await _backgroundTasks.CheckFiles();               
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
