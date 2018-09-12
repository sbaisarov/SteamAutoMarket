namespace SteamAutoMarket
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Forms;
    using SteamAutoMarket.Utils;

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

            try
            {
                Application.Run(MainForm);
            }
            catch (Exception ex)
            {
                Logger.Critical("Critical program exception", ex);
            }
        }

        private static void CheckLicense()
        {
        }

        private static void UpdateProgram()
        {
        }
    }
}