using System;
using System.Security.Cryptography;

namespace APICore.CoreBase
{
    public static class Security
    {
        /// <summary>
        /// Tạo mã MD5 cho file
        public static string GenMD5File(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
