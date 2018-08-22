using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using autotrade.WorkingProcess;
using autotrade.Utils;

namespace autotrade.CustomElements.Controls.Market {
    public partial class MarketRelistControl : UserControl {
        public MarketRelistControl() {
            InitializeComponent();

            this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
            this.AllSteamItemsGridView.Rows.Add(false, "name", "type", "21-07-1996");
        }

        public void AuthCurrentAccount() {
            this.AccountNameLable.Text = CurrentSession.SteamManager.Guard.AccountName;
            this.SplitterPanel.BackgroundImage = CurrentSession.AccountImage;
        }

        private void LoadListingButton_Click(object sender, EventArgs e) {
            if(CurrentSession.SteamManager == null) {
                MessageBox.Show("You should login first", "Error market listing loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error("Error on market listing loading. No logined account found.");
                return;
            }

            var listings = CurrentSession.SteamManager.MarketClient.MyListings();
        }
    }
}
