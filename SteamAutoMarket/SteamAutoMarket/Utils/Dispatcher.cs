namespace SteamAutoMarket.Utils
{
    using System.Windows.Forms;

    internal class Dispatcher
    {
        public delegate void AsyncAction();

        public delegate void DispatcherInvoker(Form form, AsyncAction a);

        public static void AsMainForm(AsyncAction action)
        {
            Invoke(Program.MainForm, action);
        }

        public static void AsLoadingForm(AsyncAction action)
        {
            Invoke(Program.LoadingForm, action);
        }

        public static void AsWorkingProcessForm(AsyncAction action)
        {
            Invoke(Program.WorkingProcessForm, action);
        }

        private static void Invoke(Form form, AsyncAction action)
        {
            if (form == null || action == null)
            {
                return;
            }

            if (form.InvokeRequired)
            {
                form.Invoke((DispatcherInvoker)Invoke, form, action);
            }
            else
            {
                action();
            }
        }
    }
}