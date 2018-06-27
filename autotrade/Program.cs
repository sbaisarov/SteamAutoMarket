using Market;
using Market.Enums;
using Market.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Market.SteamMarketHandler;

namespace autotrade {
    static class Program {
        static int mainThreadId;
        public static bool IsMainThread {
            get { return System.Threading.Thread.CurrentThread.ManagedThreadId == mainThreadId; }
        }
        public static Form1 MainForm;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            Application.Run(MainForm);
        }
    }
}
