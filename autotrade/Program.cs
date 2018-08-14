using Market;
using Market.Enums;
using Market.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Market.SteamMarketHandler;

namespace autotrade {
    static class Program {
        static int mainThreadId;
        public static bool IsMainThread {
            get { return Thread.CurrentThread.ManagedThreadId == mainThreadId; }
        }
        public static MainForm MainForm;
        public static WorkingProcessForm WorkingProcessForm;
        public static LoadingForm LoadingForm;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //CheckLicense();
            //UpdateProgram();
            mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
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
            //update
        }
    }
}
