using Serilog;
using Serilog.Events;

namespace LibraryManagementCleanArchitecture.Utils
{
    public static class Logger
    {
        public static void Configure()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341") 
                .CreateLogger();
        }
    }
}
