namespace SteamAutoMarket.Pages.Settings
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    using FirstFloor.ModernUI.Presentation;

    using SteamAutoMarket.Annotations;
    using SteamAutoMarket.Repository.Context;
    using SteamAutoMarket.Repository.Settings;
    using SteamAutoMarket.Utils.Resources;

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