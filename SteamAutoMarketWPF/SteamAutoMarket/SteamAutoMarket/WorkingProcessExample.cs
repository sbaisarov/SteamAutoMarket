namespace SteamAutoMarket
{
    using System.Threading;

    using Core;

    using SteamAutoMarket.Pages;
    using SteamAutoMarket.Utils.Extension;

    public class WorkingProcessExample
    {
        public static void Test()
        {
            var form = WorkingProcessForm.NewWorkingProcessWindow("Testing", 3);

            form.ProcessMethod(
                () =>
                    {
                        form.ChartModel.AddDispatch(new DataPoint(0, 0));

                        form.AppendLog("123");
                        Thread.Sleep(3000);
                        form.IncrementProgress();
                        form.ChartModel.AddDispatch(new DataPoint(2, 4));

                        form.AppendLog("456");
                        Thread.Sleep(3000);
                        form.IncrementProgress();
                        form.ChartModel.AddDispatch(new DataPoint(3, 5));

                        form.AppendLog("789");
                        Thread.Sleep(3000);
                        form.IncrementProgress();
                        form.ChartModel.AddDispatch(new DataPoint(4, 3));

                        for (var i = 5; i < 100; i++)
                        {
                            form.ChartModel.AddDispatch(new DataPoint(i, RandomUtils.RandomInt(4, 6)));
                        }
                    });
        }
    }
}