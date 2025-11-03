using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AssetManageRregister
{
    public static class LicenseManager
    {

        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("B3A2c#7!eF5d8G2jK9mN4pQ6rS8tUvX0");

        public static bool ValidateRegistrationCode(string machineCode, string code)
        {
            return code == GenerateRegistrationCode(machineCode);
        }

        public static string GenerateRegistrationCode(string machineCode)
        {
            using (HMACSHA256 hmac = new HMACSHA256(EncryptionKey))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(machineCode));
                return BitConverter.ToString(hash).Replace("-", "").Substring(0, 16);
            }
        }

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
