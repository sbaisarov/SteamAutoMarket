using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace autotrade.CustomElements.Elements
{
    public class CustomNumericUpDown : NumericUpDown
    {
        private string _leadingSign = "";
        private string _trailingSign = "%";

        /// <summary>
        ///     Gets or sets a leading symbol that is concatenate with the text.
        /// </summary>
        [Description("Gets or sets a leading symbol that is concatenated with the text.")]
        [Browsable(true)]
        [DefaultValue("")]
        public string LeadingSign
        {
            get => _leadingSign;
            set
            {
                _leadingSign = value;
                UpdateEditText();
            }
        }

        /// <summary>
        ///     Gets or sets a trailing symbol that is concatenated with the text.
        /// </summary>
        [Description("Gets or sets a trailing symbol that is concatenated with the text.")]
        [Browsable(true)]
        [DefaultValue("")]
        public string TrailingSign
        {
            get => _trailingSign;
            set
            {
                _trailingSign = value;
                UpdateEditText();
            }
        }

        protected override void UpdateEditText()
        {
            if (UserEdit) ParseEditText();

            ChangingText = true;
            Text = _leadingSign + GetNumberText(Value) + _trailingSign;
            Debug.Assert(ChangingText == false, "ChangingText should have been set to false");
        }

        private string GetNumberText(decimal num)
        {
            string text;

            if (Hexadecimal)
            {
                text = ((long) num).ToString("X", CultureInfo.InvariantCulture);
                Debug.Assert(text == text.ToUpper(CultureInfo.InvariantCulture),
                    "GetPreferredSize assumes hex digits to be uppercase.");
            }
            else
            {
                text = num.ToString(
                    (ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString(CultureInfo.CurrentCulture),
                    CultureInfo.CurrentCulture);
            }

            return text;
        }

        protected override void ValidateEditText()
        {
            ParseEditText();
            UpdateEditText();
        }

        protected new void ParseEditText()
        {
            Debug.Assert(UserEdit, "ParseEditText() - UserEdit == false");

            try
            {
                var text = Text;
                if (!string.IsNullOrEmpty(_leadingSign))
                    if (text.StartsWith(_leadingSign))
                        text = text.Substring(_leadingSign.Length);
                if (!string.IsNullOrEmpty(_trailingSign))
                    if (text.EndsWith(_trailingSign))
                        text = text.Substring(0, text.Length - _trailingSign.Length);

                if (!string.IsNullOrEmpty(text) &&
                    !(text.Length == 1 && text == "-"))
                {
                    if (Hexadecimal)
                        Value = Constrain(Convert.ToDecimal(Convert.ToInt32(text, 16)));
                    else
                        Value = Constrain(decimal.Parse(text, CultureInfo.CurrentCulture));
                }
            }
            catch
            {
            }
            finally
            {
                UserEdit = false;
            }
        }

        private decimal Constrain(decimal value)
        {
            if (value < Minimum)
                value = Minimum;

            if (value > Maximum)
                value = Maximum;

            return value;
        }
    }
}