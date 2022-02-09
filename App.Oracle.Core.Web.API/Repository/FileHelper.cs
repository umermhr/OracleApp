namespace App.Oracle.Core.Web.API.Repository
{
    public class FileHelper : IFileHelper
    {
        private readonly IConfiguration _configuration;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IWebHostEnvironment _env;
        private readonly OracleConnection _oracleConnection;

        public FileHelper(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
            _oracleConnection = new OracleConnection(_configuration["ConnectionStrings:OracleConnection"]);
        }

        public void ReadFileContent()
        {
            try
            {
                var folderPath = _configuration["AppSettings:FolderPath"];
                var backupFolderPath = _configuration["AppSettings:BackupFolderPath"];
                _logger.Info($"start checking directory ({folderPath}) for txt files.");
                var filePaths = Directory.GetFiles(folderPath);
                if (filePaths.Length ==  0)
                {
                    _logger.Info($"no file found for processing.");
                    return;
                }
                _logger.Info($"total files found in {folderPath}: {filePaths.Length}");
                foreach (var filePath in filePaths)
                {
                    var fileInfo = new FileInfo(filePath);
                    _logger.Info($"starting processing file {fileInfo.Name}");
                    if (fileInfo.Extension != ".txt")
                    {
                        fileInfo.MoveTo($"{backupFolderPath}\\{fileInfo.Name}", true);
                        _logger.Info($"skipped file {fileInfo.Name} because it is not txt file.");
                    }
                    _logger.Info($"creating master file record in database.");
                    var fileId = InsertMasterFileRecord(fileInfo.Name, fileInfo.CreationTime, "SYSTEM");
                    _logger.Info($"master file record created in database with file id: {fileId}.");

                    _logger.Info($"start reading and saving content for file {fileInfo.Name}.");
                    var fileContent = File.ReadAllLines(filePath, Encoding.UTF8);
                    var lineNo = 1;
                    foreach(var line in fileContent)
                    {
                        InsertFileContentRecord(fileId, lineNo, line, "SYSTEM");
                        lineNo++;
                    }
                    _logger.Info($"finished reading file {fileInfo.Name}.");
                    fileInfo.MoveTo($"{backupFolderPath}\\{fileInfo.Name}", true);
                    _logger.Info($"finished processing file {fileInfo.Name}");
                }

                _logger.Info($"finished checking directory ({folderPath}) for txt files.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public IEnumerable<FileMaster> GetFileList()
        {
            IList<FileMaster> response = new List<FileMaster>();
            try
            {                
                OracleCommand oracleCommand = _oracleConnection.CreateCommand();
                _oracleConnection.Open();
                oracleCommand.CommandText = "files_get_all";
                oracleCommand.CommandType = CommandType.StoredProcedure;
                var dataReader = oracleCommand.ExecuteReader();
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
            finally
            {
                _oracleConnection.Close();
            }
            return response;
        }

        public IEnumerable<FileContent> GetFileContentByFileId(int fileId)
        {
            IList<FileContent> response = new List<FileContent>();
            try
            {
                OracleCommand oracleCommand = _oracleConnection.CreateCommand();
                _oracleConnection.Open();
                oracleCommand.CommandText = "get_file_content_by_file_id";
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Add("v_file_id", OracleDbType.Int32, fileId, ParameterDirection.Input);
                var dataReader = oracleCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    var fileContent = new FileContent
                    {
                        Id = dataReader.GetInt32(0),
                        FileId = dataReader.GetInt32(1),
                        LineNo = dataReader.GetInt32(2),
                        LineContent = dataReader.GetString(3),
                        RecordCreationTime = dataReader.GetDateTime(4),
                        RecordCreatedBy = dataReader.GetString(5)
                    };
                    response.Add(fileContent);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _oracleConnection.Close();
            }
            return response;
        }

        private int InsertMasterFileRecord(string fileName, DateTime fileCreation, string user)
        {
            var fileId = 0;
            try
            {
                OracleCommand oracleCommand = _oracleConnection.CreateCommand();
                _oracleConnection.Open();
                oracleCommand.CommandText = "insert_file_master";
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Add("v_filename", OracleDbType.Varchar2, fileName, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_file_creation", OracleDbType.TimeStamp, fileCreation, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_record_by", OracleDbType.Varchar2, user, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_file_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                var affectedRows = oracleCommand.ExecuteNonQuery();
                var id = oracleCommand.Parameters["v_file_id"].Value;
                fileId = Convert.ToInt32(oracleCommand.Parameters["v_file_id"].Value.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _oracleConnection.Close();
            }
            return fileId;
        }

        private int InsertFileContentRecord(int fileId, int lineNo, string lineContent, string user)
        {
            var affectedRows = 0;
            try
            {
                OracleCommand oracleCommand = _oracleConnection.CreateCommand();
                _oracleConnection.Open();
                oracleCommand.CommandText = "insert_file_content";
                oracleCommand.CommandType = CommandType.StoredProcedure;
                oracleCommand.Parameters.Add("v_file_id", OracleDbType.Int32, fileId, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_line_no", OracleDbType.Int32, lineNo, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_line_content", OracleDbType.Varchar2, lineContent, ParameterDirection.Input);
                oracleCommand.Parameters.Add("v_record_by", OracleDbType.Varchar2, user, ParameterDirection.Input);
                affectedRows = oracleCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                _oracleConnection.Close();
            }
            return affectedRows;
        }
    }
}
