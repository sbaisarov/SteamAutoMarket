using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autotrade.CustomElements.Elements {
    class ComboboxWithImage : ComboBox {
        Dictionary<int, Image> IMAGES_DICTIONARY = new Dictionary<int, Image>();

        public void AddItem(string text, Image image) {
            int index = Items.Add(text);
            IMAGES_DICTIONARY.Add(index, image);
        }

        public Image GetImageByIndex(int index) {
            return IMAGES_DICTIONARY[index];
        }
    }
}
