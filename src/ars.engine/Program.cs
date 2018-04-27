using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using Topshelf;
using System.Configuration;


namespace ars.engine
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Serilog.Debugging.SelfLog.Enable(s => Trace.WriteLine($"SERILOG ERROR: {s}"));

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useLogFile"]))
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .WriteTo.RollingFile(@".\Logs\log-{Date}.txt")
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .WriteTo.EventLog(ConfigurationManager.AppSettings["appName"], manageEventSource: true)
                    .WriteTo.ColoredConsole()
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }

            HostFactory.Run(x =>
            {
                x.UseSerilog();
                x.Service<ArsEngine>(s =>
                {
                    s.ConstructUsing(name => new ArsEngine());
                    s.WhenStarted(tc => tc.StartService());
                    s.WhenStopped(tc => tc.StopService());
                });

                x.UseLinuxIfAvailable();
                x.StartAutomatically();
                x.RunAsLocalSystem();
                x.SetDescription(ConfigurationManager.AppSettings["appDesc"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["appName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["appServiceName"]);
            });
        }
    }
}
