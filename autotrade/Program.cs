using System;
using System.Threading;
using System.Windows.Forms;
using SteamAutoMarket.CustomElements.Forms;

namespace SteamAutoMarket
{
    internal static class Program
    {
        private static int _mainThreadId;

        public static MainForm MainForm;
        public static WorkingProcessForm WorkingProcessForm;
        public static LoadingForm LoadingForm;

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == _mainThreadId;

        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //CheckLicense();
            //UpdateProgram();
            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm();
            WorkingProcessForm = new WorkingProcessForm();
            LoadingForm = new LoadingForm();

            Application.Run(MainForm);
        }

        private static void CheckLicense()
        {
            throw new NotImplementedException();
        }

        private static void UpdateProgram()
        {
            throw new NotImplementedException();
        }
    }
}