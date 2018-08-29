using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ars.lib.Messages;
using Akka.Actor;
using System.Diagnostics;
using System.IO;

namespace ars.lib.Actors
{
    public class ScannerPrimaryActor : UntypedActor
    {
        private readonly long _instanceNum;
        private readonly string tempExePath = Path.Combine(Path.GetTempPath(), "masscan.exe");

        public ScannerPrimaryActor(long instanceNum)
        {
            _instanceNum = instanceNum;
        }
        protected override void OnReceive(object message)
        {
            switch(message)
            {
                case RequestMetadata m:
                    Sender.Tell(new ResponseMetadata(m.RequestId, _instanceNum));
                    break;
                case RequestPrimaryScanBegin m:
                    ExtractPrimaryScanner();
                    Sender.Tell(new RespondPrimaryScanComplete());
                    break;
                default:
                    break;
            }
        }

        public static Props Props(long instanceNum) =>
            Akka.Actor.Props.Create(() => new ScannerPrimaryActor(instanceNum)); 

        internal void ExtractPrimaryScanner()
        {

            if (!File.Exists(tempExePath))
            {
                File.WriteAllBytes(tempExePath, ars.lib.Properties.Resources.masscan);
            }
        }


    }
}
