namespace SteamAutoMarket
{
    using System;
    using System.Windows.Forms;

    using SteamAutoMarket.CustomElements.Forms;
    using SteamAutoMarket.Utils;

    internal class Program
    {
        public static MainForm MainForm { get; set; }

        public static WorkingProcessForm WorkingProcessForm { get; set; }

        public static LoadingForm LoadingForm { get; set; }

        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                LoadingForm = new LoadingForm();
                MainForm = new MainForm();
                Application.Run(MainForm);
             }
             catch (Exception ex)
             {
                 Logger.Critical("Critical program exception", ex);
             }
        }
    }
}