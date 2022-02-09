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
    }
}
