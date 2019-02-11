namespace SteamAutoMarket.UI.Pages.Settings
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    using FirstFloor.ModernUI.Presentation;

    using SteamAutoMarket.Localization;
    using SteamAutoMarket.Properties;
    using SteamAutoMarket.UI.Repository.Context;
    using SteamAutoMarket.UI.Repository.Settings;
    using SteamAutoMarket.UI.Utils;
    using SteamAutoMarket.UI.Utils.Resources;

    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : INotifyPropertyChanged
    {
        private Color selectedAccentColor;

        private Link selectedTheme;

        public Appearance()
        {
            this.InitializeComponent();

            this.DataContext = this;

            UiGlobalVariables.Appearance = this;

            this.AccentColors = UiDefaultValues.Colors.Values.ToArray();

            this.SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += this.OnAppearanceManagerPropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Color[] AccentColors { get; }

        public Color SelectedAccentColor
        {
            get => this.selectedAccentColor;
            set
            {
                if (this.selectedAccentColor == value) return;
                this.selectedAccentColor = value;
                this.OnPropertyChanged();
                SettingsProvider.GetInstance().Color = ModernUiThemeUtils.GetColorName(value);
                AppearanceManager.Current.AccentColor = value;
            }
        }

        public Link SelectedTheme
        {
            get => this.selectedTheme;
            set
            {
                if (this.selectedTheme == value) return;
                this.selectedTheme = value;
                this.OnPropertyChanged();
                SettingsProvider.GetInstance().Theme = value.DisplayName;
                AppearanceManager.Current.ThemeSource = value.Source;
            }
        }

        public LinkCollection Themes { get; } = UiDefaultValues.Themes;

        public int WorkingChartMaxCount
        {
            get => SettingsProvider.GetInstance().WorkingChartMaxCount;
            set
            {
                SettingsProvider.GetInstance().WorkingChartMaxCount = value;
                this.OnPropertyChanged();
            }
        }

        public IEnumerable<string> Languages { get; } = UiDefaultValues.Languages;

        public string SelectedLanguage
        {
            get => SettingsProvider.GetInstance().Local;
            set
            {
                SettingsProvider.GetInstance().Local = value;
                this.OnPropertyChanged();
                SettingsUpdated.ForceSettingsUpdate();
                this.LocalizeApplication();
            }
        }

        private void LocalizeApplication()
        {
            StringsProvider.UpdateLocal();

            UiGlobalVariables.MainWindow?.LocalizeMenuLinks();
            UiGlobalVariables.SettingsPage?.LocalizeMenuLinks();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                this.SyncThemeAndColor();
            }
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme =
                this.Themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
        }
    }
}