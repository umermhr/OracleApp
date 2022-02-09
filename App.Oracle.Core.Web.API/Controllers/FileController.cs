using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Oracle.Core.Web.API.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileHelper _fileHelper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IWebHostEnvironment _env;

        public FileController(IFileHelper fileHelper, IWebHostEnvironment env)
        {
            _fileHelper = fileHelper;
            _env = env;
        }

        [Route(template: "checkforfiles")]
        [HttpGet]
        //[BasicAuth]
        public IActionResult CheckForFiles()
        {
            try
            {
                _fileHelper.ReadFileContent();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return NotFound();
        }

        [Route(template: "getlogfile")]
        [HttpGet]
        //[BasicAuth]
        public IActionResult GetLogFile()
        {
            try
            {
                var logFile = _fileHelper.ReadLogFile();
                if (logFile != null)
                {
                    return Ok(value: logFile);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return NotFound();
        }

        [Route(template: "downloadlogfile")]
        [HttpGet]
        //[BasicAuth]
        public IActionResult DownloadLogFile()
        {
            try
            {
                var logFilePath = _fileHelper.GetLogFilePath();
                var fileInfo = new FileInfo(logFilePath);
                if (fileInfo.Exists)
                {
                    var fileDownload = new FileDownload
                    {
                        ContentType = "application/octet-stream",
                        FileName = fileInfo.Name,
                        FileContent = System.IO.File.ReadAllBytes(logFilePath)
                    };
                    return Ok(fileDownload);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return NotFound();
        }

        [Route(template: "getlist")]
        [HttpGet]
        //[BasicAuth]
        public IActionResult GetList()
        {
            try
            {
                var list = _fileHelper.GetFileList();
                if (list != null && list.Any())
                {
                    return Ok(value: list);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return NotFound();
        }

        [Route(template: "getfilecontent/{fileId?}")]
        [HttpGet]
        //[BasicAuth]
        public IActionResult GetFileContent([FromRoute] int fileId)
        {
            try
            {
                var list = _fileHelper.GetFileContentByFileId(fileId);
                if (list != null && list.Any())
                {
                    return Ok(value: list);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return NotFound();
        }
    }
}
