using Dorisoy.AMS.models;
using Dorisoy.AMS.view;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Dorisoy.AMS
{
    // 新增全局上下文类1
    public static class AppContext
    {

        public static User CurrentUser { get; private set; }

        public static void Initialize(User user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user));
        }
    }

    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 加载语言包（优先执行）
            LoadFastReportLocalization();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool createdNew;
            using (Mutex mutex = new Mutex(true, "Dorisoy.AMSInstanceMutex", out createdNew))
            {
                if (!createdNew)
                {
                    ActivateExistingInstance();
                    return;
                }
                // 前置许可证检查
                if (!Utilities.LicenseManager.CheckLicense())
                {
                    MessageBox.Show("软件授权验证后请重新登录，本程序即将退出");
                    Environment.Exit(1); // 强制终止进程
                }

                // 初始化数据库
                SqliteHelper.InitDb();

                // 用户登录流程
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        AppContext.Initialize(loginForm.CurrentUser);
                        Application.Run(new MainForm());
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static void ActivateExistingInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process.Id != currentProcess.Id && process.MainWindowHandle != IntPtr.Zero)
                {
                    ShowWindow(process.MainWindowHandle, 9);
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }
            }
        }

        private static void LoadFastReportLocalization()
        {
            string chineseFrlPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Chinese (Simplified).frl"
            );

            if (File.Exists(chineseFrlPath))
            {
                FastReport.Utils.Res.LoadLocale(chineseFrlPath);
            }
            else
            {
                MessageBox.Show("缺失中文语言包文件！");
            }
        }
    }
}

