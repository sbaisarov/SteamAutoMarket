namespace SteamAutoMarket
{
    using System.Threading;

    using SteamAutoMarket.Pages;

    public class WorkingProcessExample
    {
        public static void Test()
        {
            var form = WorkingProcessForm.NewWorkingProcessWindow("Testing", 3);

            form.ProcessMethod(
                () =>
                    {
                        form.AppendLog("123");
                        Thread.Sleep(3000);
                        form.IncrementProgress();

                        form.AppendLog("456");
                        Thread.Sleep(3000);
                        form.IncrementProgress();

                        form.AppendLog("789");
                        Thread.Sleep(3000);
                        form.IncrementProgress();
                    });
        }
    }
}