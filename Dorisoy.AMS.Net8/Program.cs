using Dorisoy.AMS.models;
using Dorisoy.AMS.view;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dorisoy.AMS
{
    /// <summary>
    /// 新增全局上下文类
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// 初始化应用程序上下文
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
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
        
        /// <summary>
        /// 显示主窗体
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 显示前窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 激活已存在的应用程序实例
        /// </summary>
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

        /// <summary>
        /// 加载本地报表
        /// </summary>
        private static void LoadFastReportLocalization()
        {
            //C:\Users\Administrator\Downloads\asset-management-net8\Dorisoy.AMSNet8\Chinese (Simplified).frl
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