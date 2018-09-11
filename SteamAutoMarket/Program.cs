using System.IO;

namespace SteamAutoMarket
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Forms;

    internal static class Program
    {
        private static int mainThreadId;

        public static MainForm MainForm { get; set; }

        public static WorkingProcessForm WorkingProcessForm { get; set; }

        public static LoadingForm LoadingForm { get; set; }

        public static bool IsMainThread => Thread.CurrentThread.ManagedThreadId == mainThreadId;

        [STAThread]
        private static void Main()
        {
            CheckLicense();
            UpdateProgram();
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new MainForm();
            LoadingForm = new LoadingForm();

            Application.Run(MainForm);
        }

        private static void CheckLicense()
        {
        }

        private static void UpdateProgram()
        {
        }
    }
}