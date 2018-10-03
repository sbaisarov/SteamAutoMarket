using TelegramShop.Qiwi.QiwiApi.Entities.Profile;

namespace SteamAutoMarket
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;

    using Newtonsoft.Json;
    using AutoUpdaterDotNET;

    using SteamAutoMarket.CustomElements.Forms;
    using SteamAutoMarket.Utils;

    internal static class Program
    {
        public static MainForm MainForm { get; set; }

        public static WorkingProcessForm WorkingProcessForm { get; set; }

        public static LoadingForm LoadingForm { get; set; }

        [STAThread]
        private static void Main()
        {

            UpdateProgram();
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

        private static bool CheckLicense()
        {
            // какой-то хардкор с пост запросом на шарпе
            var wb = WebRequest.Create("https://www.steambiz.store/api/checklicense");
            wb.Method = "POST";
            var data = new NameValueCollection { ["key"] = "5c2ef522-c99a-487e-b624-652bd9fabacd" };

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
                // let the user know that software can't resolve the hardware id
            }

            data["hwid"] = uid;
            var postDataString = string.Empty;
            foreach (string key in data)
            {
                postDataString += key + "&=" + data[key];
            }

            var postData = Encoding.UTF8.GetBytes(postDataString);
            var dataStream = wb.GetRequestStream();
            dataStream.Write(postData, 0, postData.Length);
            dataStream.Close();
            var resp = wb.GetResponse();
            if (((HttpWebResponse)resp).StatusDescription != "OK")
            {
                // show failed license checking to the user and quit
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

        private static void UpdateProgram()
        {
            ServicePointManager.ServerCertificateValidationCallback += 
                (sender, certificate, chain, sslPolicyErrors) => true;  // NOT FOR PRODUCTION
//            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.Start("https://www.steambiz.store/release/release.xml");
        }
        
        private static void AutoUpdater_ApplicationExitEvent()
        {
            // let the user know that update has finished and he should launch the software again.

//            foreach (string file in Directory.EnumerateFiles(Directory.GetCurrentDirectory()))
//            {
//                try
//                {
//                    if (!file.EndsWith("SteamAutoMarket.zip")) File.Delete(file);
//                }
//                catch (Exception e) {}
//            }

//            Application.Exit();
        }
    }
}