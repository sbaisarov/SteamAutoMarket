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
    }
}
