using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AssetManageRregister
{
    /// <summary>
    ///  license 授权管理器
    /// </summary>
    public static class LicenseManager
    {
        /// <summary>
        /// 解key
        /// </summary>
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("B3A2c#7!eF5d8G2jK9mN4pQ6rS8tUvX0");
        
        /// <summary>
        /// 验证注册码
        /// </summary>
        /// <param name="machineCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool ValidateRegistrationCode(string machineCode, string code)
        {
            return code == GenerateRegistrationCode(machineCode);
        }

        /// <summary>
        /// 生产注册码
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public static string GenerateRegistrationCode(string machineCode)
        {
            // Hahah 365
            using (HMACSHA256 hmac = new HMACSHA256(EncryptionKey))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(machineCode));
                return BitConverter.ToString(hash).Replace("-", "").Substring(0, 16);
            }
        }

        /// <summary>
        /// 哈希字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string HashString(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }
    }
}
