using Akka.Actor;
using ars.lib.Common.Interfaces;
using ars.lib.Common.Utils;
using Serilog;
using System;
using System.Diagnostics;
using SaltwaterTaffy;
using SaltwaterTaffy.Container;
using System.Linq;

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
                
                //db.InitializeDB();

                arsSystem = ActorSystem.Create("ARS-System");
                Log.Information("{SysName} started.", arsSystem.Name);

                TestScan();

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


        internal void TestScan()
        {
            Log.Information("Beginning scan ...");
            var target = new Target("192.168.1.0/24");
            ScanResult result = new Scanner(target, ProcessWindowStyle.Hidden).PortScan();

            foreach(Host i in result.Hosts)
            {
                Log.Information("Host: {ipAddress}", i.Address);

                foreach (Port j in i.Ports)
                {
                    Log.Information("port {portNum} {portService} {portFiltered}", j.PortNumber, "is running " + ((!string.IsNullOrEmpty(j.Service.Name)) ? j.Service.Name : string.Empty), (j.Filtered) ? "is filtered" : string.Empty);                   

                }

                if (i.OsMatches.Any())
                {
                    Log.Information("And is probably running {osName}", i.OsMatches.First().Name);
                }

            }
        }


    }
}
