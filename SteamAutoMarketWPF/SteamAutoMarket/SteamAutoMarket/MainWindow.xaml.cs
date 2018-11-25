using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using AutoUpdaterDotNET;
using Newtonsoft.Json;

namespace SteamAutoMarket
{
    using System;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    using Core;

    using FirstFloor.ModernUI.Presentation;
    using FirstFloor.ModernUI.Windows.Controls;

    using log4net.Config;

    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Logger;
    using SteamAutoMarket.Utils.Resources;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            ServicePointManager.ServerCertificateValidationCallback += OnServerCertificateValidationCallback;
            SettingsProvider.GetInstance();
            XmlConfigurator.Configure();
            Logger.UpdateLoggerLevel(SettingsProvider.GetInstance().LoggerLevel);
            AppearanceManager.Current.ThemeSource = ModernUiThemeUtils.GetTheme(SettingsProvider.GetInstance().Theme);
            AppearanceManager.Current.AccentColor = ModernUiThemeUtils.GetColor(SettingsProvider.GetInstance().Color);
            UiGlobalVariables.MainWindow = this;
            this.DataContext = this;
            this.InitializeComponent();
            
            if (!File.Exists("license.txt"))
            {
                throw new UnauthorizedAccessException("Cant get requered info");
            }

            var main = File.ReadAllText("license.txt");
            if (!this.Check(main))
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            this.UpdateProgram();
        }
        
        public bool Check(string main)
        {
            var wb = (HttpWebRequest) WebRequest.Create("\u0068\u0074\u0074\u0070\u0073\u003a\u002f\u002f\u0077\u0077\u0077\u002e\u0073\u0074\u0065\u0061\u006d\u0062\u0069\u007a\u002e\u0073\u0074\u006f\u0072\u0065\u002f\u0061\u0070\u0069\u002f\u0063\u0068\u0065\u0063\u006b\u006c\u0069\u0063\u0065\u006e\u0073\u0065");
            wb.Method = "POST";
            wb.ContentType = "application/x-www-form-urlencoded";
            var data = new NameValueCollection { ["key"] = main };

            // read key from user database.
            // read from the input field if key is not present in user database
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            var uid = moc.Cast<ManagementObject>().Select(x => x.Properties["processorID"]).FirstOrDefault()?.Value
                .ToString();

            try
            {
                var dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
                dsk.Get();
                uid += dsk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                throw new UnauthorizedAccessException("Cant get requered info");
            }

            data["hwid"] = uid;
            var postDataString = string.Empty;
            foreach (string key in data)
            {
                postDataString += key + "=" + data[key] + "&";
            }
            
            postDataString = postDataString.Trim("&".ToCharArray());
            var postData = Encoding.UTF8.GetBytes(postDataString);
            var dataStream = wb.GetRequestStream();
            dataStream.Write(postData, 0, postData.Length);
            dataStream.Close();
            var resp = wb.GetResponse();
            if (((HttpWebResponse)resp).StatusDescription != "OK")
            {
                throw new UnauthorizedAccessException("Access denied");
            }

            dataStream = resp.GetResponseStream();
            var reader = new StreamReader(dataStream);
            dynamic responseJson = JsonConvert.DeserializeObject(
                Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(reader.ReadToEnd())));
            dataStream.Close();
            reader.Close();
            resp.Close();
            return responseJson["success_3248237582"];
        }

        private void UpdateProgram()
        {
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.Start("https://www.steambiz.store/release/release.xml");
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // todo add critical error send here
            ErrorNotify.CriticalMessageBox("Oops. Seems application is crushed", (Exception)e.ExceptionObject);
        }

        private static bool OnServerCertificateValidationCallback(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (certificate.Subject.Contains("pythonanywhere")
                && certificate.GetCertHashString() == "6889BBFB104CE4CC4D35400B309C9526B85CB69D")
            {
                return true;
            }

            return sslPolicyErrors.ToString() == "None";
        }
    }
}