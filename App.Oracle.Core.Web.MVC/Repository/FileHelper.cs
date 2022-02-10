namespace App.Oracle.Core.Web.MVC.Repository
{
    public class FileHelper : IFileHelper
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;

        public FileHelper(IConfiguration configuration, HttpClient httpClient, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _env = env;
        }

        public async Task<IEnumerable<FileMaster>> GetListAsync()
        {
            IList<FileMaster>? responseObj = null;
            try
            {
                responseObj = await (await _httpClient.GetAsync(requestUri: $"file/getlist")).Content.ReadFromJsonAsync<FileMaster[]>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return responseObj;
        }

        public async Task<IEnumerable<FileContent>> GetFileContentAsync(int fileId)
        {
            IList<FileContent>? responseObj = null;
            try
            {
                responseObj = await (await _httpClient.GetAsync(requestUri: $"file/getfilecontent/{fileId}")).Content.ReadFromJsonAsync<FileContent[]>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return responseObj;
        }

        public async Task<LogFile> GetLogFileAsync()
        {
            LogFile? responseObj = null;
            try
            {
                responseObj = await (await _httpClient.GetAsync(requestUri: $"file/getlogfile")).Content.ReadFromJsonAsync<LogFile>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return responseObj;
        }

        public async Task<FileDownload> DownloadLogFileAsync()
        {
            FileDownload? responseObj = null;
            try
            {
                responseObj = await (await _httpClient.GetAsync(requestUri: $"file/downloadlogfile")).Content.ReadFromJsonAsync<FileDownload>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return responseObj;
        }
    }
}
