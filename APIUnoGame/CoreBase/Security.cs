using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace APIGame.CoreBase
{
    public static class Security
    {
        #region MD5
        /// <summary>
        /// Tạo mã MD5 cho file
        /// </summary>
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

        /// <summary>
        /// Tạo mã MD5 từ string
        /// </summary>
        public static string GenMD5String(string str)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
        #endregion

        #region Encryption

        private const string passPhrase = "BlackFace13"; // can be any string
        private const string saltValue = "sALtValue"; // can be any string
        private const string hashAlgorithm = "SHA1"; // can be "MD5"
        private const int passwordIterations = 7; // can be any number
        private const string initVector = "~1B2c3D4e5F6g7H8"; // must be 16 bytes
        private const int keySize = 256; // can be 192 or 128

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(initVector);
            var rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            var buffer = Encoding.UTF8.GetBytes(data);
#pragma warning disable 618
            var rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
#pragma warning restore 618
            var managed = new RijndaelManaged { Mode = CipherMode.CBC };
            var transform = managed.CreateEncryptor(rgbKey, bytes);
            var stream = new MemoryStream();
            var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray).Replace("=", "~").Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Decrypt(string data)
        {
            data = data.Replace('~', '=').Replace('-', '+').Replace('_', '/');

            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(data);
#pragma warning disable 618
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
#pragma warning restore 618
            var managed = new RijndaelManaged { Mode = CipherMode.CBC };
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            var stream = new MemoryStream(buffer);
            var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            var buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }
        #endregion
    }
}
