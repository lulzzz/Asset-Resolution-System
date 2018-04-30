using ars.lib.Messages;
using Akka.Actor;

namespace ars.lib.Actors
{
    public class IP_Processor_Actor : UntypedActor
    {
        private long _instanceNum;

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
                default:
                    break;
            }
        }

        public static Props Props(long instanceNum) =>
            Akka.Actor.Props.Create(() => new IP_Processor_Actor(instanceNum));
    }
}
