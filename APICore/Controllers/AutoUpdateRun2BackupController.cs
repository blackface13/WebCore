using APICore.CoreBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Controllers
{
    [Route("[controller]")]
    public class AutoUpdateRun2BackupController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Run2Backup API, created by Black Face");
        }

        /// <summary>
        /// Kiểm tra phiên bản mới
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckingVersion")]
        public IActionResult CheckingVersion()
        {
            try
            {
                //Khởi tạo version
                if (string.IsNullOrEmpty(AutoUpdateSettings.Run2BackupCurentVersion))
                    AutoUpdateSettings.Run2BackupCurentVersion = FileVersionInfo.GetVersionInfo(AutoUpdateSettings.Run2BackupExeFile).ProductVersion;
                return Ok(AutoUpdateSettings.Run2BackupCurentVersion);
            }
            catch (Exception ex)
            {
                return Ok("Lỗi: " + ex);
            }
        }

        [HttpGet("CreateUpdate")]
        public IActionResult CreateUpdate()
        {
            AutoUpdateSettings.Run2BackupCurentVersion = FileVersionInfo.GetVersionInfo(AutoUpdateSettings.Run2BackupExeFile).ProductVersion;
            return Ok("Tạo tự động cập nhật thành công cho phiên bản " + AutoUpdateSettings.Run2BackupCurentVersion);
        }

        [HttpGet("PushLog/{ip}")]
        public IActionResult PushLog(string ip)
        {
            using (StreamWriter file = System.IO.File.AppendText(AutoUpdateSettings.Run2BackupLogFileName))
            {
                file.WriteLine(DateTime.Now.ToString() + ": " + ip);
                file.Flush();
                file.Close();
            }
            return Ok("");
        }

        /// <summary>
        /// Download bản mới về
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Upgrade")]
        public async Task<FileStreamResult> Upgrade(int id)
        {
            try
            {
                var stream = System.IO.File.OpenRead(AutoUpdateSettings.Run2BackupExeFile);
                return new FileStreamResult(stream, "application/octet-stream") { FileDownloadName = "NewVersion.exe" };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
