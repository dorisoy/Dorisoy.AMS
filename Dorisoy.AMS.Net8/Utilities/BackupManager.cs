using SqlSugar;
using System.Configuration;

namespace Dorisoy.AMS.Utilities
{
    public class BackupManager
    {
        public static string CurrentDbPath { get; } = "AssetDB.sqlite";
        public static string BackupRecordFile { get; } = "backup.cfg";
        public static string BackupPath => ConfigurationManager.AppSettings["BackupLocation"] ?? @".\Backup";
        private static SqlSugarClient? _db;
        private static DateTime? _lastBackupDate;

        // 新增静态方法：检查是否需要执行启动备份
        public static void CheckAndPerformStartupBackup()
        {
            DateTime? lastBackupDate = null;
            if (File.Exists(BackupRecordFile))
            {
                string lastBackupDateString = File.ReadAllText(BackupRecordFile);
                if (DateTime.TryParse(lastBackupDateString, out DateTime parsedDate))
                {
                    lastBackupDate = parsedDate;
                }
            }

            // 条件触发备份
            if (!lastBackupDate.HasValue || lastBackupDate.Value.Date != DateTime.Now.Date)
            {
                // 通过实例调用非静态方法
                PerformBackup();
            }
        }

        public static void InitializeBackupDirectory()
        {
            try
            {
                Directory.CreateDirectory(BackupManager.BackupPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"目录创建失败：{ex.Message}");
            }
        }

        public BackupManager()
        {
            _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = $"DataSource={CurrentDbPath}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            });
        }

        public class BackupFileInfo
        {
            public string FileName { get; set; } = string.Empty;
            public DateTime CreateTime { get; set; }
            public long FileSizeKB { get; set; }
            public string FullPath { get; set; } = string.Empty;
        }

        public static List<BackupFileInfo> GetBackupList()
        {
            return Directory.GetFiles(BackupPath, "*.db")
                .Where(f => Path.GetFileName(f).Contains("_备份_"))
                .Select(f => new BackupFileInfo
                {
                    FileName = Path.GetFileName(f),
                    CreateTime = File.GetCreationTime(f),
                    FileSizeKB = new FileInfo(f).Length / 1024,
                    FullPath = f
                })
                .OrderByDescending(f => f.CreateTime)
                .ToList();
        }

        public static void PerformBackup()
        {
            try
            {
                Directory.CreateDirectory(BackupPath);
                var dbName = Path.GetFileNameWithoutExtension(CurrentDbPath);
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var backupFilePath = Path.Combine(BackupPath, $"{dbName}_备份_{timestamp}.db");

                _db?.Dispose();
                File.Copy(CurrentDbPath, backupFilePath, true);
                UpdateBackupDate();
            }
            finally
            {
                ResetDbConnection();
            }
        }

        public static void RestoreBackup(string backupFilePath)
        {
            try
            {
                _db?.Dispose();
                File.Copy(backupFilePath, CurrentDbPath, true);
            }
            finally
            {
                ResetDbConnection();
            }
        }

        public static void CleanOldBackups(int retainDays)
        {
            // 防止没有backup文件夹报错
            InitializeBackupDirectory();
            var cutoffDate = DateTime.Now.AddDays(-retainDays);
            var files = Directory.GetFiles(BackupPath, "*.db")
                .Where(f => File.GetCreationTime(f) < cutoffDate);
            foreach (var file in files)
            {
                try { File.Delete(file); }
                catch { /* 日志记录 */ }
            }
        }

        private static void UpdateBackupDate()
        {
            _lastBackupDate = DateTime.Now;
            // 更新静态字段以记录上次备份日期
            File.WriteAllText(BackupRecordFile, _lastBackupDate.Value.ToString());
        }

        private static void ResetDbConnection()
        {
            _db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = $"DataSource={CurrentDbPath}",
                DbType = SqlSugar.DbType.Sqlite,
                IsAutoCloseConnection = true
            });
        }
    }
}