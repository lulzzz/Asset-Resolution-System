
namespace ars.lib.Messages
{
    public sealed class ResponseMetadata
    {
        public long RequestId { get; }
        public long InstanceNum { get; }

        public ResponseMetadata(long requestId, long instanceNum)
        {
            RequestId = requestId;
            InstanceNum = instanceNum;
        }
    }
}
