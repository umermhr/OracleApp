using App.Oracle.Core.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Oracle.Core.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileHelper _fileHelper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public HomeController(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            try
            {
                var logFile = await _fileHelper.GetLogFileAsync();
                return View(logFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Download()
        {
            try
            {
                var logFile = await _fileHelper.DownloadLogFileAsync();
                return File(logFile.FileContent, logFile.ContentType, logFile.FileName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Files()
        {
            try
            {
                var files = await _fileHelper.GetListAsync();
                return View(files);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        [HttpGet]
        public async Task<ViewResult> FileContent(int fileId)
        {
            try
            {
                var fileContent = await _fileHelper.GetFileContentAsync(fileId);
                return View(fileContent);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}