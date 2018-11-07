namespace SteamAutoMarket.Pages
{
    using SteamAutoMarket.Repository.Context;

    /// <summary>
    /// Interaction logic for WorkingProcessForm.xaml
    /// </summary>
    public partial class WorkingProcessForm
    {
        private WorkingProcessForm()
        {
            this.InitializeComponent();

            this.DataContext = this;
        }

        public static void NewWorkingProcessWindow()
        {
            var window = new WorkingProcessForm();
            window.Show();
        }
    }
}
