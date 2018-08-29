using ars.lib.Messages;
using Akka.Actor;
using System.Net;

namespace ars.lib.Actors
{
    public class IP_Processor_Actor : UntypedActor
    {
        private readonly long _instanceNum;

        public IP_Processor_Actor(long instanceNum)
        {
            _instanceNum = instanceNum;
        }

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case RequestMetadata m:
                    Sender.Tell(new ResponseMetadata(m.RequestId, _instanceNum));
                    break;
                case RequestIPMetadata m:
                    Sender.Tell(new ResponseIPMetadata(ResolveAddresses(m.IpAddress)));
                    break;
                default:
                    break;
            }
        }

        public static Props Props(long instanceNum) =>
            Akka.Actor.Props.Create(() => new IP_Processor_Actor(instanceNum));

        internal IPNetwork ResolveAddresses(string ipaddress)
        {
            if (IPNetwork.TryParse(ipaddress, out IPNetwork ipnetwork1))
            {
                return ipnetwork1;
            }

            return null;            
        }

    }
}
