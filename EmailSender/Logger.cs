using Microsoft.VisualBasic;
using Serilog;

namespace EmailSender.Services
{
    public static class Logger
    {
        static Logger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit : 30)
                .CreateLogger();
        }

        public static void Info(string message)
        {
            Log.Information(message);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }
    }
}
