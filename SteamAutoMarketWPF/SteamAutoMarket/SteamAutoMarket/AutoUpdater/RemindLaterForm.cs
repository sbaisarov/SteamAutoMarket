namespace SteamAutoMarket.AutoUpdater
{
    using System;
    using System.Windows.Forms;

    internal partial class RemindLaterForm : Form
    {
        public RemindLaterForm()
        {
            this.InitializeComponent();
        }

        public int RemindLaterAt { get; private set; }

        public RemindLaterFormat RemindLaterFormat { get; private set; }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (this.radioButtonYes.Checked)
            {
                switch (this.comboBoxRemindLater.SelectedIndex)
                {
                    case 0:
                        this.RemindLaterFormat = RemindLaterFormat.Minutes;
                        this.RemindLaterAt = 30;
                        break;
                    case 1:
                        this.RemindLaterFormat = RemindLaterFormat.Hours;
                        this.RemindLaterAt = 12;
                        break;
                    case 2:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 1;
                        break;
                    case 3:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 2;
                        break;
                    case 4:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 4;
                        break;
                    case 5:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 8;
                        break;
                    case 6:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 10;
                        break;
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void RadioButtonYesCheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxRemindLater.Enabled = this.radioButtonYes.Checked;
        }

        private void RemindLaterFormLoad(object sender, EventArgs e)
        {
            this.comboBoxRemindLater.SelectedIndex = 0;
            this.radioButtonYes.Checked = true;
        }
    }
}