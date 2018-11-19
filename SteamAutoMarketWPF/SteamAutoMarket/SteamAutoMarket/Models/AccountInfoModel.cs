﻿namespace SteamAutoMarket.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using SteamAutoMarket.Annotations;

    public class AccountInfoModel : INotifyPropertyChanged
    {
        private string name;

        private string value;

        public AccountInfoModel(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public AccountInfoModel(string name, object value)
        {
            this.name = name;
            this.value = value?.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => this.name;
            set
            {
                if (value == this.name) return;
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        public string Value
        {
            get => this.value;
            set
            {
                if (value == this.value) return;
                this.value = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}