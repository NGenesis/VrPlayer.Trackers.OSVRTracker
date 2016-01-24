using System;
using System.IO;
using System.Reflection;
using log4net;

namespace VrPlayer.Helpers
{
    public class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Logger _instance;
        public static Logger Instance
        {
            get { return _instance ?? (_instance = new Logger()); }
        }

        private Logger()
        {
            var configFileInfo = new FileInfo("Log4Net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(configFileInfo);
        }
        
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception);
        }
    }
}
