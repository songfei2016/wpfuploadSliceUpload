using System;
using System.Threading.Tasks;
using System.Windows;
using Serilog;
using St.Common;
using System.Threading;

namespace St.Upload
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        private bool isNewInstance = true;
        public static Mutex Mutex1 { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            CheckAppInstance();
            LogManager.CreateLogFile();
            if (Current.ShutdownMode != ShutdownMode.OnExplicitShutdown)
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            var bootstrapper = new DevBootstrapper();

            bootstrapper.Run();
        }

        private void CheckAppInstance()
        {
            Mutex1 = new Mutex(true, "StUpload", out isNewInstance);

            if (!isNewInstance)
            {
                SscDialog dialog = new SscDialog("程序已经在运行中！");
                dialog.ShowDialog();
                Current.Shutdown();
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            Log.Logger.Error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Log.Logger.Error($"【unhandled exception】：{exception}");
        }

        private void Current_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Logger.Error("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Log.Logger.Error($"【unhandled exception】：{e.Exception}");
            MessageBox.Show("当前应用程序遇到一些问题，将要退出！", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);
            e.Handled = true;
        }
    }
}
