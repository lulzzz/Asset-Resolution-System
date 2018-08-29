
namespace ars.lib.Messages
{
    public sealed class RequestIPMetadata
    {
        public string IpAddress { get; set; }

        public RequestIPMetadata(string ipaddress)
        {
            IpAddress = ipaddress;
        }
    }
}
