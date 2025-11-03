using Dorisoy.AMS.view;
using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Dorisoy.AMS.Utilities
{
    public static class LicenseManager
    {
        private const string LicenseFile = "license.dat";
        public const int TrialDays = 15;
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("B3A2c#7!eF5d8G2jK9mN4pQ6rS8tUvX0");
        private static readonly byte[] IV = new byte[16];

        public class LicenseInfo
        {
            public DateTime FirstRunDate { get; set; }
            public bool IsRegistered { get; set; }
            public DateTime? RegistrationDate { get; set; }
            public string MachineCode { get; set; }
            public string RegistrationCode { get; set; }
        }

        public static bool CheckLicense()
        {
            var license = LoadLicense();

            // 已注册设备验证
            if (license.IsRegistered)
            {
                return ValidateHardware(license);
            }

            // 试用期计算（精确到整天）
            var elapsedDays = (DateTime.Today - license.FirstRunDate.Date).TotalDays;
            if (elapsedDays > TrialDays)
            {
                // 强制进入注册流程
                using (var regForm = new RegistrationForm(license.ToString()))
                {
                    if (regForm.ShowDialog() == DialogResult.OK)
                    {
                        SaveLicense(license);
                        return true;
                    }
                    return false;
                }
            }

            // 试用期提醒
            ShowTrialWarning((int)(TrialDays - elapsedDays));
            return true;
        }

        private static bool ValidateHardware(LicenseInfo license)
        {
            return GenerateMachineCode() == license.MachineCode;
        }

        private static void ShowTrialWarning(int remainingDays)
        {
            if (remainingDays <= 3)
            {
                MessageBox.Show($"剩余试用天数：{remainingDays}\n请及时注册",
                    "试用提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static bool ValidateRegistration(LicenseInfo license)
        {
            var currentMachineCode = GenerateMachineCode();
            if (license.MachineCode != currentMachineCode)
            {
                return HandleInvalidLicense("硬件环境发生变化，请重新注册！");
            }

            if (!ValidateRegistrationCode(currentMachineCode, license.RegistrationCode))
            {
                return HandleInvalidLicense("无效的注册码！");
            }

            return true;
        }

        private static bool ShowRegistration(LicenseInfo license)
        {
            using (var form = new RegistrationForm(license.MachineCode))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (ValidateRegistrationCode(license.MachineCode, form.RegistrationCode))
                    {
                        license.IsRegistered = true;
                        license.RegistrationDate = DateTime.Now;
                        license.RegistrationCode = form.RegistrationCode;
                        SaveLicense(license);
                        return true;
                    }
                    MessageBox.Show("无效的注册码！");
                }
            }
            return false;
        }

        private static bool HandleInvalidLicense(string message)
        {
            MessageBox.Show(message + "\n程序将退出！", "许可证错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        #region 加密存储
        public static LicenseInfo LoadLicense()
        {
            if (File.Exists(LicenseFile))
            {
                try
                {
                    var encrypted = File.ReadAllText(LicenseFile);
                    var json = Decrypt(encrypted);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<LicenseInfo>(json);
                }
                catch
                {
                    // 文件损坏时创建新许可证
                    return CreateNewLicense();
                }
            }
            return CreateNewLicense();
        }

        private static LicenseInfo CreateNewLicense()
        {
            var license = new LicenseInfo
            {
                FirstRunDate = DateTime.Now,
                MachineCode = GenerateMachineCode(),
                IsRegistered = false
            };
            SaveLicense(license);
            return license;
        }

        public static void SaveLicense(LicenseInfo license)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(license);
            var encrypted = Encrypt(json);
            File.WriteAllText(LicenseFile, encrypted);
        }
        #endregion

        #region 加密算法
        private static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = EncryptionKey;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private static string Decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = EncryptionKey;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
        #endregion

        #region 硬件信息
        public static string GenerateMachineCode()
        {
            var sb = new StringBuilder();
            try
            {
                sb.Append(GetCpuId());
                sb.Append(GetDiskSerial());
                sb.Append(GetMotherboardSerial());
            }
            catch
            {
                sb.Append(Environment.MachineName);
                sb.Append(Environment.ProcessorCount);
            }
            return HashString(sb.ToString());
        }

        private static string GetCpuId()
        {
            using (var searcher = new ManagementObjectSearcher("Select ProcessorId From Win32_processor"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    return mo["ProcessorId"].ToString();
                }
            }
            return string.Empty;
        }

        private static string GetDiskSerial()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    return mo["SerialNumber"].ToString();
                }
            }
            return string.Empty;
        }

        private static string GetMotherboardSerial()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    return mo["SerialNumber"].ToString();
                }
            }
            return string.Empty;
        }

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
        #endregion

        // 用于演示生成注册码的方法
        public static void GenerateAndShowRegistrationCode()
        {
            string machineCode = GenerateMachineCode();
            string registrationCode = GenerateRegistrationCode(machineCode);

            Console.WriteLine("Machine Code: " + machineCode);
            Console.WriteLine("Registration Code: " + registrationCode);
        }
    }
}
