using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace ars.console.Routes
{
    public class ApiModule : NancyModule
    {
        public ApiModule() : base("/api")
        {
            Get["/"] = _ =>
            {
                return "<H3>Hello from ApiModule</H3>";
            };
        }
    }
}
