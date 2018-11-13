namespace SteamAutoMarket.Pages
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using Core;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;

    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class LogsWindow : INotifyPropertyChanged
    {
        private static string logs;

        public LogsWindow()
        {
            this.InitializeComponent();
            this.DataContext = this;
            UiGlobalVariables.LogsWindow = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static string GlobalLogs
        {
            get => logs;
            set
            {
                if (UiGlobalVariables.LogsWindow != null)
                {
                    UiGlobalVariables.LogsWindow.Logs = value;
                }
                else
                {
                    logs = value;
                }
            }
        }

        public string Logs
        {
            get => logs;
            set
            {
                logs = value;
                this.OnPropertyChanged();
                if (this.ScrollLogsToEnd)
                {
                    Application.Current.Dispatcher.Invoke(() => this.LogTextBox.ScrollToEnd());
                }
            }
        }

        public bool ScrollLogsToEnd
        {
            get => SettingsProvider.GetInstance().ScrollLogsToEnd;
            set
            {
                SettingsProvider.GetInstance().ScrollLogsToEnd = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenLogFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            Logger.Log.Debug("Debug");
            Logger.Log.Info("Info");
            Logger.Log.Warn("Warn");
            Logger.Log.Error("Error");
            Logger.Log.Fatal("Fatal");
        }
    }
}