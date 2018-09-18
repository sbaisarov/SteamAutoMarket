using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using RestSharp.Deserializers;

using SteamKit2.Internal;

namespace SteamAutoMarket
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Windows.Forms;

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
            /*var access = CheckLicense();
            if (!access)
            {
                // show the message and quit
            }
            */

            // UpdateProgram();
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
            var data = new NameValueCollection();

            // read key from user database.
            // read from the input field if key is not present in user database
            data["key"] = "5c2ef522-c99a-487e-b624-652bd9fabacd";
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
            return responseJson["sucess"];
        }

        private static void UpdateProgram()
        {
            // не тестировал, не уверен будет ли правильно работать

            // сверить с версией бд и версией на удаленном сервере. Если разные - обновить
            var request = WebRequest.Create("https://software-assembly.com");
            var response = request.GetResponse(); // get zip archive
            var webStream = response.GetResponseStream();
            Debug.Assert(webStream != null, nameof(webStream) + " != null");
            var archive = new ZipArchive(webStream);
            archive.ExtractToDirectory(Directory.GetCurrentDirectory());
        }
    }
}