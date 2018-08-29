using ars.lib.Common.Interfaces;
using ars.lib.Common.Utils;
using Autofac;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using Topshelf;

namespace ars.console
{
    class Program
    {
        private static readonly IContainer Container = lib.Common.Utils.Container.Initialize();
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Serilog.Debugging.SelfLog.Enable(s => Trace.WriteLine($"SERILOG ERROR: {s}"));

            using (var scope = Container.BeginLifetimeScope())
            {
                var settings = scope.Resolve<ISettings>();
                var db = scope.Resolve<Ars_DAL>();

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
                    x.Service<ArsConsole>(s =>
                    {
                        s.ConstructUsing(name => new ArsConsole());
                        s.WhenStarted(tc => tc.StartService(settings, db));
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
}

