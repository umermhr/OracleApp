namespace App.Oracle.Core.Web.API.Repository
{
    public class FileHelper : IFileHelper
    {
        private readonly IConfiguration _configuration;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IWebHostEnvironment _env;
        private readonly OracleHelper _oracleHelper;

        public FileHelper(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            _oracleHelper = new OracleHelper(_configuration["ConnectionStrings:OracleConnection"]);
        }

        public string[]? ReadFileContent(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return null;
        }

        public IEnumerable<FileMaster> GetFileList()
        {
            IList<FileMaster> response = new List<FileMaster>();
            try
            {
                var dataReader = _oracleHelper.ExecuteReader("files_get_all");
                while (dataReader.Read())
                {
                    var fileMaster = new FileMaster
                    {
                        Id = dataReader.GetInt32(0),
                        FileName = dataReader.GetString(1),
                        FileCreationTime = dataReader.GetDateTime(2),
                        RecordCreationTime = dataReader.GetDateTime(3),
                        RecordCreatedBy = dataReader.GetString(4)
                    };
                    response.Add(fileMaster);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return response;
        }
    }
}
