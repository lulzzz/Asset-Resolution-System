using Akka.Actor;
using ars.lib.Common.Interfaces;
using ars.lib.Common.Utils;
using Microsoft.Owin.Hosting;
using Serilog;
using System;
using System.Diagnostics;

namespace ars.console
{
    internal class ArsConsole
    {
        private ActorSystem arsSystem;
        private IDisposable app;

        internal bool StartService(ISettings settings, Ars_DAL db)
        {
            try
            {
                Log.Information("Starting ARS System ...");

                db.InitializeDB();

                arsSystem = ActorSystem.Create("ARS-System");
                Log.Information("{SysName} started.", arsSystem.Name);

                var appAddress = "http://localhost:5000/";
                app = WebApp.Start(appAddress);

                Log.Information("{SysName} listening at {AppAddress}", arsSystem.Name, appAddress);
                return true;
            }
            catch (Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                string MethodName = sf.GetMethod().Name;
                Log.Error(ex, "Get {MethodName} failed: {ErrMsg}", MethodName, ex.Message);
                return false;
            }
        }

        internal bool StopService()
        {
            Log.Information("Shutting down system.");
            app.Dispose();
            arsSystem.Terminate();
            Log.CloseAndFlush();
            return true;
        }
    }
}
