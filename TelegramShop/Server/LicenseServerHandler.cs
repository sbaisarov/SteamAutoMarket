namespace TelegramShop.Server
{
    using System.Collections.Generic;

    public class LicenseServerHandler
    {
        public static bool IsLicenseExist(string licenseKey)
        {
            return true; // todo
        }

        public static string GetLicenseStatus(HashSet<string> userLicenseKeys)
        {
            return "Some info from license server"; // todo
        }

        public static void AddNewLicense(string licenseKey, int days)
        {
            // todo
        }

        public static void AddDaysForExistLicense(string licenseKey, int days)
        {
            // todo
        }
    }
}