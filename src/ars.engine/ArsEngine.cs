using Akka.Actor;
using ars.lib.Common.Interfaces;
using ars.lib.Common.Utils;
using Serilog;
using System;
using System.Diagnostics;

namespace ars.engine
{
    internal class ArsEngine
    {
        private ActorSystem arsSystem;

        internal bool StartService(ISettings settings, Ars_DAL db)
        {
            try
            {
                Log.Information("Starting ARS System ...");
                
                db.InitializeDB();

                arsSystem = ActorSystem.Create("ARS-System");
                Log.Information("{SysName} started.", arsSystem.Name);
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
            arsSystem.Terminate();
            Log.CloseAndFlush();
            return true;
        }


    }
}
