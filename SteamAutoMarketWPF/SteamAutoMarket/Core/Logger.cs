namespace Core
{
    using System.Reflection;

    using log4net;

    public class Logger
    {
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}