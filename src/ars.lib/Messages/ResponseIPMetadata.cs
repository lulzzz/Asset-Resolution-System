using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ars.lib.Messages
{
    public sealed class ResponseIPMetadata
    {
        public IPNetwork Network { get; set; }

        public ResponseIPMetadata(IPNetwork network)
        {
            Network = network;
        }
    }
}
