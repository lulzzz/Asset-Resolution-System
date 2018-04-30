using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using Topshelf;
using System.Configuration;
using System.Collections.Specialized;
using Autofac;
using ars.lib.Common.Utils;
using ars.lib.Common.Interfaces;

namespace ars.engine
{
    class Program
    {
        private static readonly IContainer Container = lib.Common.Utils.Container.Initialize();
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Serilog.Debugging.SelfLog.Enable(s => Trace.WriteLine($"SERILOG ERROR: {s}"));

            var scope = Container.BeginLifetimeScope();
            var settings = scope.Resolve<ISettings>();

            if (Convert.ToBoolean(settings.UseLogFile))
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.RollingFile(@".\Logs\log-{Date}.txt")
                    .WriteTo.ColoredConsole()
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.EventLog(settings.AppName, manageEventSource: true)
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
                x.SetDescription(settings.AppDesc);
                x.SetDisplayName(settings.AppName);
                x.SetServiceName(settings.AppServiceName);
            });
        }
    }
}
