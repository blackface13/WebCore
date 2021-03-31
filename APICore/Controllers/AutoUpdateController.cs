using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APICore.CoreBase;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Xml.Linq;
using System.Diagnostics;

namespace APICore.Controllers
{
    [Route("[controller]")]
    public class AutoUpdateController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("BlackFace API");
        }

        /// <summary>
        /// Khởi tạo hoặc chạy lại tự động cập nhật cho phiên bản mới nhất
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateUpdate")]
        public IActionResult CreateUpdate()
        {
            try
            {
                //Đọc all file trong thư mục
                var allfile4 = Directory.GetFiles(AutoUpdateSettings.SoftwarePath, "*", SearchOption.AllDirectories);
                var allinfor = new Dictionary<string, string>();
                for (int i = 0; i < allfile4.Length; i++)
                {
                    allinfor.Add(allfile4[i].Replace(AutoUpdateSettings.SoftwarePath, ""), Security.GenMD5File(allfile4[i]));
                }

                //Clear danh sách trước khi tạo mới
                try
                {
                    AutoUpdateSettings.AutoUpdateListFile.Clear();
                }
                catch
                {
                    AutoUpdateSettings.AutoUpdateListFile = new List<string>();
                }

                //Tạo file mới và đẩy vào list
                using (StreamWriter file = new StreamWriter(AutoUpdateSettings.InforUpdateFileName))
                    foreach (var entry in allinfor)
                    {
                        var result = string.Format("{0};{1}", entry.Key, entry.Value);
                        AutoUpdateSettings.AutoUpdateListFile.Add(result);
                        file.WriteLine(result);
                    }

                //Gán phiên bản mới nhất
                AutoUpdateSettings.CurentVersion = FileVersionInfo.GetVersionInfo(AutoUpdateSettings.ExeFile).ProductVersion;

                return Ok("Tạo tự động cập nhật thành công cho phiên bản " + AutoUpdateSettings.CurentVersion);
            }
            catch (Exception ex)
            {
                return Ok("Lỗi: " + ex);
            }
        }

        /// <summary>
        /// Trả về phiên bản mới nhất có trên server
        /// </summary>
        /// <returns></returns>
        [HttpGet("Check")]
        public IActionResult GetFileInfor()
        {
            try
            {
                if (string.IsNullOrEmpty(AutoUpdateSettings.CurentVersion))
                    CreateUpdate();
                return Ok(AutoUpdateSettings.CurentVersion);
            }
            catch (Exception ex)
            {
                return Ok("Lỗi: " + ex);
            }
        }


        /// <summary>
        /// Download file
        /// </summary>
        /// <returns></returns>
        [HttpGet("getfile/{id}")]
        public async Task<FileStreamResult> Download(int id)
        {
            try
            {
                //Đọc danh sách file
                var fileList = System.IO.File.ReadAllLines(AutoUpdateSettings.InforUpdateFileName);

                var path = AutoUpdateSettings.AutoUpdateListFile[id].Split(';')[0];
                var stream = System.IO.File.OpenRead(AutoUpdateSettings.SoftwarePath + path);

                return new FileStreamResult(stream, "application/octet-stream") { FileDownloadName = Path.GetFileName(path) };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Tải file thông tin autoupdate về
        /// </summary>
        /// <returns></returns>
        [HttpGet("getuploadfileinfor")]
        public async Task<FileStreamResult> DownloadUpdateFileInfor()
        {
            var stream = System.IO.File.OpenRead(AutoUpdateSettings.InforUpdateFileName);
            return new FileStreamResult(stream, "application/octet-stream") { FileDownloadName = Path.GetFileName(AutoUpdateSettings.InforUpdateFileName) };
        }
    }
}
