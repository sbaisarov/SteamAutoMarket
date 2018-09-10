namespace SteamAutoMarket.CustomElements.Elements
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ComboboxWithImage : ComboBox
    {
        private readonly Dictionary<int, Image> imagesDictionary = new Dictionary<int, Image>();

        public void AddItem(string text, Image image)
        {
            var index = this.Items.Add(text);
            this.imagesDictionary.Add(index, image);
        }

        public Image GetImageByIndex(int index)
        {
            return this.imagesDictionary[index];
        }
    }
}