namespace TelegramShop.Telegram
{
    using System.Collections.Generic;
    using System.Linq;

    using global::Telegram.Bot.Types.ReplyMarkups;

    internal class KeyboardBuilder
    {
        private readonly Dictionary<int, string[]> buttonsDictionary = new Dictionary<int, string[]>();

        public KeyboardBuilder Add(params string[] buttons)
        {
            this.buttonsDictionary[this.buttonsDictionary.Count + 1] = buttons;
            return this;
        }

        public ReplyKeyboardMarkup GetKeyboard()
        {
            var keyboardLinesList = new List<IEnumerable<KeyboardButton>>();

            foreach (var line in this.buttonsDictionary.OrderBy(e => e.Key))
            {
                var keyboardLine = line.Value.Select(s => new KeyboardButton(s));
                keyboardLinesList.Add(keyboardLine);
            }

            return new ReplyKeyboardMarkup(keyboardLinesList);
        }

        public InlineKeyboardMarkup GetInlineKeyboard()
        {
            var keyboardLinesList = new List<IEnumerable<InlineKeyboardButton>>();

            foreach (var line in this.buttonsDictionary.OrderBy(e => e.Key))
            {
                var keyboardLine = line.Value.Select(s => new InlineKeyboardButton { Text = s,  });
                keyboardLinesList.Add(keyboardLine);
            }

            return new InlineKeyboardMarkup(keyboardLinesList);
        }
    }
}