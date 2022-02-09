namespace App.Oracle.Core.Worker.Service.Repository
{
    public class BackgroundTasks : IBackgroundTasks
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public BackgroundTasks(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(uriString: _configuration[key: "WebService:BaseUri"]);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                 AuthenticationSchemes.Basic.ToString(),
                 Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CipherHelper.Decrypt(_configuration[key: "WebService:Username"])}:{CipherHelper.Decrypt(_configuration[key: "WebService:Password"])}"))
                 );
        }

        public async Task<int> CheckFiles()
        {
            var statusCode = 0;
            try
            {
                statusCode = int.Parse(await (await _httpClient.GetAsync(requestUri: "file/checkforfiles")).Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return statusCode;
        }
    }
}
