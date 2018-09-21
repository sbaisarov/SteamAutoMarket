namespace TelegramShop.Telegram
{
    using System;
    using System.Threading.Tasks;

    using global::Telegram.Bot.Args;

    using TelegramShop.ShopUser;
    using TelegramShop.Telegram.MessageProcessor;

    public class SpamHandler
    {
        public static async Task<bool> IsSpamming(
            TelegramShopClient telegramShop,
            MessageEventArgs e,
            ShopUserModel user)
        {
            // if user is blocked
            if (user.SpamWarning >= 100 && StillBlocked(user))
            {
                return true;
            }

            // if user is not spamming
            if (ShopUserRepository.IsUserSpamming(user) == false)
            {
                ShopUserRepository.UpdateUserLastMessageDate(user);
                return false;
            }

            ShopUserRepository.UpdateUserLastMessageDate(user);
            ShopUserRepository.UpdateSpamLevelWarning(user);

            // if we should ban user
            if (user.SpamWarning >= 10)
            {
                ShopUserRepository.UpdateSpamLevelWarning(user, 100);

                await telegramShop.SendMessage(e.Message.Chat.Id, AnswerMessage.SpamYouAreBlockedMessage, null);

                return true;
            }

            await telegramShop.SendMessage(
                e.Message.Chat.Id,
                AnswerMessage.SpamWarningMessage.Replace("{times}", (10 - user.SpamWarning).ToString()),
                TelegramShopMessageHandler.GetKeyboard(user.CurrentDialogState));

            return true;
        }

        public static bool StillBlocked(ShopUserModel user)
        {
            var minutes = (DateTime.Now - user.LastMessageDate).TotalMinutes;
            var stillBlocked = minutes < 60;

            if (stillBlocked == false)
            {
                ShopUserRepository.UpdateSpamLevelWarning(user, 0);
            }

            return stillBlocked;
        }
    }
}