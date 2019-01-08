namespace SteamAutoMarket.UI.Repository.Context
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using SteamAutoMarket.UI.Pages;
    using SteamAutoMarket.UI.Utils.Logger;

    [Obfuscation(Exclude = true)]
    public static class WorkingProcessProvider
    {
        public static readonly WorkingProcessDataContext EmptyWorkingProcess =
            new WorkingProcessDataContext("Working process", null);

        public static readonly List<WorkingProcessDataContext> ExistWorkingProcesses =
            new List<WorkingProcessDataContext>();

        public static IEnumerable<string> GetAllProcessesNames() => ExistWorkingProcesses.Select(p => p.Title);

        public static WorkingProcessDataContext GetInstance(string name) =>
            ExistWorkingProcesses.FirstOrDefault(x => x.Title == name) ?? EmptyWorkingProcess;

        public static WorkingProcessDataContext GetNewInstance(string title)
        {
            var steamManager = UiGlobalVariables.SteamManager;
            var workingProcessTitle = $"{steamManager.Login} {title}";

            var wp = ExistWorkingProcesses.FirstOrDefault(w => w.Title == workingProcessTitle);

            if (wp != null)
            {
                if (wp.WorkingAction.IsCompleted)
                {
                    wp.ResetWorkingProcessToDefault();
                }
                else
                {
                    ErrorNotify.CriticalMessageBox(
                        $"{workingProcessTitle} working process is already running! Two identical working processes can not be started at the same time!");
                    return null;
                }
            }
            else
            {
                wp = new WorkingProcessDataContext(workingProcessTitle, steamManager);
                ExistWorkingProcesses.Add(wp);
            }

            return wp;
        }

        public static void RemoveWorkingProcessFromList(string title)
        {
            if (title == null) return;
            ExistWorkingProcesses.RemoveAll(p => p.Title == title);
        }
    }
}