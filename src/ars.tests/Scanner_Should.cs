using Xunit;
using Akka.TestKit.Xunit2;
using ars.lib.Messages;
using ars.lib.Actors;
using System.IO;

namespace ars.tests
{
    public class Scanner_Should : TestKit
    {
        [Fact]
        public void InitializeScannerPrimaryMetaData()
        {
            var probe = CreateTestProbe();

            var ipProc = Sys.ActorOf(ScannerPrimaryActor.Props(1));

            ipProc.Tell(new RequestMetadata(1), probe.Ref);

            var received = probe.ExpectMsg<ResponseMetadata>();

            Assert.Equal(1, received.RequestId);
        }

        [Fact]
        public void ExtractPrimaryScanner()
        {
            var probe = CreateTestProbe();

            var ipProc = Sys.ActorOf(ScannerPrimaryActor.Props(1));

            ipProc.Tell(new RequestPrimaryScanBegin(), probe.Ref);

            var received = probe.ExpectMsg<RespondPrimaryScanComplete>();

            var tempExePath = Path.Combine(Path.GetTempPath(), "masscan.exe");

            Assert.True(File.Exists(tempExePath));
        }
    }
}
