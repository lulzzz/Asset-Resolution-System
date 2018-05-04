using Xunit;
using Akka.TestKit.Xunit2;
using ars.lib.Messages;
using ars.lib.Actors;

namespace ars.tests
{
    public class IP_Should : TestKit
    {
        [Fact]
        public void InitializeIPProcessorMetaData()
        {
            var probe = CreateTestProbe();

            var ipProc = Sys.ActorOf(IP_Processor_Actor.Props(1));

            ipProc.Tell(new RequestMetadata(1), probe.Ref);

            var received = probe.ExpectMsg<ResponseMetadata>();

            Assert.Equal(1, received.RequestId);
        }
    }
}
