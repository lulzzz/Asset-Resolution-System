using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Nancy.Owin;
using Owin;

[assembly: OwinStartup(typeof(ars.console.Startup))]

namespace ars.console
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            var config = new HubConfiguration()
            {
                EnableJavaScriptProxies = true
            };

            
            app.MapSignalR(config); //enable websocket magic
            app.UseNancy();
        }
    }
}
