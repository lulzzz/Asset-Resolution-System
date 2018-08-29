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

        [Fact]
        public void ResolveNetworkAddresses()
        {
            var probe = CreateTestProbe();
            var ipProc = Sys.ActorOf(IP_Processor_Actor.Props(1));

            ipProc.Tell(new RequestIPMetadata("192.168.1.0/29"), probe.Ref);

            var received = probe.ExpectMsg<ResponseIPMetadata>();

            Assert.NotNull(received);
            Assert.NotNull(received.Network);

            using (var ips = received.Network.ListIPAddress())
            {
                Assert.Equal("192.168.1.0", ips[0].ToString());
                Assert.Equal("192.168.1.1", ips[1].ToString());
                Assert.Equal("192.168.1.2", ips[2].ToString());
                Assert.Equal("192.168.1.3", ips[3].ToString());
                Assert.Equal("192.168.1.4", ips[4].ToString());
                Assert.Equal("192.168.1.5", ips[5].ToString());
                Assert.Equal("192.168.1.6", ips[6].ToString());
                Assert.Equal("192.168.1.7", ips[7].ToString());
            }
        }

        [Fact]
        public void NotResolveBadAddresses()
        {
            var probe = CreateTestProbe();
            var ipProc = Sys.ActorOf(IP_Processor_Actor.Props(1));

            ipProc.Tell(new RequestIPMetadata("123123123123"), probe.Ref);

            var received = probe.ExpectMsg<ResponseIPMetadata>();

            Assert.NotNull(received);
            Assert.Null(received.Network);
        }
    }
}
