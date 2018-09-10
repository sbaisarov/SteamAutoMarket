namespace SteamAutoMarket.CustomElements.Elements
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows.Forms;

    public class CustomNumericUpDown : NumericUpDown
    {
        private string leadingSign = string.Empty;

        private string trailingSign = "%";

        /// <summary>
        ///     Gets or sets a leading symbol that is concatenate with the text.
        /// </summary>
        [Description("Gets or sets a leading symbol that is concatenated with the text.")]
        [Browsable(true)]
        [DefaultValue("")]
        public string LeadingSign
        {
            get => this.leadingSign;
            set
            {
                this.leadingSign = value;
                this.UpdateEditText();
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
            get => this.trailingSign;
            set
            {
                this.trailingSign = value;
                this.UpdateEditText();
            }
        }

        protected override void UpdateEditText()
        {
            if (this.UserEdit)
            {
                this.ParseEditText();
            }

            this.ChangingText = true;
            this.Text = this.leadingSign + this.GetNumberText(this.Value) + this.trailingSign;
        }

        protected override void ValidateEditText()
        {
            this.ParseEditText();
            this.UpdateEditText();
        }

        protected new void ParseEditText()
        {
            try
            {
                var text = this.Text;
                if (!string.IsNullOrEmpty(this.leadingSign) && text.StartsWith(this.leadingSign))
                {
                    text = text.Substring(this.leadingSign.Length);
                }

                if (!string.IsNullOrEmpty(this.trailingSign) && text.EndsWith(this.trailingSign))
                {
                    text = text.Substring(0, text.Length - this.trailingSign.Length);
                }

                if (string.IsNullOrEmpty(text) || (text.Length == 1 && text == "-"))
                {
                    return;
                }

                this.Value = this.Constrain(
                    this.Hexadecimal
                        ? Convert.ToDecimal(Convert.ToInt32(text, 16))
                        : decimal.Parse(text, CultureInfo.CurrentCulture));
            }
            finally
            {
                this.UserEdit = false;
            }
        }

        private string GetNumberText(decimal num)
        {
            string text;

            if (this.Hexadecimal)
            {
                text = ((long)num).ToString("X", CultureInfo.InvariantCulture);
                Debug.Assert(
                    text == text.ToUpper(CultureInfo.InvariantCulture),
                    "GetPreferredSize assumes hex digits to be uppercase.");
            }
            else
            {
                text = num.ToString(
                    (this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString(CultureInfo.CurrentCulture),
                    CultureInfo.CurrentCulture);
            }

            return text;
        }

        private decimal Constrain(decimal value)
        {
            if (value < this.Minimum)
            {
                value = this.Minimum;
            }

            if (value > this.Maximum)
            {
                value = this.Maximum;
            }

            return value;
        }
    }
}