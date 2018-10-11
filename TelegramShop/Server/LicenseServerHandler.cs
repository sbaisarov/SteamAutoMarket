using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace TelegramShop.Server
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Net;

    public class LicenseServerHandler
    {
        private static readonly WebClient Wb = new WebClient()
        {
            Headers = {"Authorization",
                $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes("steambiz777 XgnLJjQ0X5cG"))}"}
        };
        
        public static bool IsLicenseExist(string licenseKey)
        {
            var response = Wb.UploadString("https://www.steambiz.store/api/getlicensestatus",
                licenseKey);
            response = JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result"];
            return response != null;
        }

        public static string GetLicenseStatus(HashSet<string> userLicenseKeys)
        {
            var response = Wb.UploadString("https://www.steambiz.store/api/getlicensestatus",
                string.Join(",", userLicenseKeys));
            response = JsonConvert.DeserializeObject<IDictionary<string, string>>(response)["result"];
            return response;
        }

        public static string GetNewLicense(int days)
        {
            return Wb.DownloadString($"https://www.steambiz.store/api/getlicense/{days}");
        }

        public static void AddDaysForExistLicense(string licenseKey, int days)
        {
            Wb.UploadString("https://www.steambiz.store/api/extendlicense", licenseKey + "," + days);
        }
    }
}