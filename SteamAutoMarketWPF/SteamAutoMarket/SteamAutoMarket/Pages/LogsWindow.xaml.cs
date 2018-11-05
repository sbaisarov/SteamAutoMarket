namespace SteamAutoMarket.Pages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class LogsWindow : UserControl
    {
        public LogsWindow()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private void OpenLogFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            //UiGlobalVariables.Logs += "Some text" + Environment.NewLine;
        }
    }
}