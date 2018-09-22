namespace TelegramShop.ShopUser
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Types;

    using Newtonsoft.Json;

    using TelegramShop.Telegram;

    using File = System.IO.File;

    public class ShopUserRepository
    {
        private static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory + "users.json";

        private static Dictionary<string, ShopUserModel> cachedShopUserModels;

        private static bool isUpdateFileAlreadyPlaned;

        public static Dictionary<string, ShopUserModel> GetAllUsers()
        {
            if (cachedShopUserModels != null)
            {
                return cachedShopUserModels;
            }

            if (!File.Exists(FilePath))
            {
                File.Create(FilePath);
            }

            var file = File.ReadAllText(FilePath);
            cachedShopUserModels = JsonConvert.DeserializeObject<Dictionary<string, ShopUserModel>>(file);

            if (cachedShopUserModels == null)
            {
                cachedShopUserModels = new Dictionary<string, ShopUserModel>();
            }

            return cachedShopUserModels;
        }

        public static ShopUserModel GetUser(User user)
        {
            var telegramId = GetUserTelegramId(user);

            if (GetAllUsers().ContainsKey(telegramId))
            {
                return GetAllUsers()[telegramId];
            }

            return CreateAndCacheUser(telegramId);
        }

        public static bool IsUserSpamming(ShopUserModel userModel)
        {
            var seconds = (DateTime.Now - userModel.LastMessageDate).TotalSeconds;
            return seconds < 0.5;
        }

        public static void UpdateUserLastMessageDate(ShopUserModel user)
        {
            user.LastMessageDate = DateTime.Now;

            UpdateFile();
        }

        public static void UpdateUserDialogState(ShopUserModel user, EDialogState state)
        {
            user.CurrentDialogState = state;

            UpdateFile();
        }

        public static void AddLicenseKey(ShopUserModel user, string licenseKey)
        {
            user.LicenseKeys.Add(licenseKey);

            UpdateFile();
        }

        public static void RemoveLicenseKey(ShopUserModel user, string licenseKey)
        {
            user.LicenseKeys.Remove(licenseKey);

            UpdateFile();
        }

        public static void UpdateUserBuyLicenseKey(ShopUserModel user, string licenseKey)
        {
            user.LicenseBuyProcess.LicenseKey = licenseKey;

            UpdateFile();
        }

        public static void UpdateUserLicenseDuration(ShopUserModel user, int days, double price)
        {
            user.LicenseBuyProcess.Days = days;
            user.LicenseBuyProcess.Price = price;

            UpdateFile();
        }

        public static void UpdatePurchaseUniqueComment(ShopUserModel user, string comment)
        {
            user.LicenseBuyProcess.Comment = comment;

            UpdateFile();
        }

        public static void UpdateSpamLevelWarning(ShopUserModel user, int? level = null)
        {
            if (level.HasValue == false)
            {
                user.SpamWarning++;
            }
            else
            {
                user.SpamWarning = level.Value;
                GetAllUsers()[user.Telegram].SpamWarning = level.Value;
            }

            UpdateFile();
        }

        public static void UpdateFile()
        {
            if (isUpdateFileAlreadyPlaned)
            {
                return;
            }

            WriteToFile();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void WriteToFile()
        {
            Task.Run(
                () =>
                    {
                        isUpdateFileAlreadyPlaned = true;

                        Thread.Sleep(5000);

                        var json = JsonConvert.SerializeObject(GetAllUsers(), Formatting.Indented);
                        try
                        {
                            File.WriteAllText(FilePath, json);
                        }
                        catch (Exception)
                        {
                            isUpdateFileAlreadyPlaned = false;
                            WriteToFile();
                        }

                        isUpdateFileAlreadyPlaned = false;
                    });
        }

        private static string GetUserTelegramId(User user)
        {
            if (!string.IsNullOrEmpty(user.Username))
            {
                return user.Username;
            }

            return user.Id.ToString();
        }

        private static ShopUserModel CreateAndCacheUser(string telegramId)
        {
            var newUser = new ShopUserModel { Telegram = telegramId };
            GetAllUsers().Add(telegramId, newUser);
            UpdateFile();

            return newUser;
        }
    }
}