using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using ScriptCs.Contracts;

namespace ScriptCs.SignalR
{
    public class SignalR : IScriptPackContext
    {
        public IDisposable CreateServer(string baseAddress)
        {
            return WebApplication.Start<Startup>(baseAddress);
        }

        public IDisposable CreateServer()
        {
            return WebApplication.Start<Startup>("http://localhost:8080");
        }

        public IDisposable CreateServer<TStartup>(string baseAddress)
        {
            return WebApplication.Start<TStartup>();            
        }

        public IDisposable CreateServer<TStartup>()
        {
            return WebApplication.Start<TStartup>("http://localhost:8080");
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration { EnableCrossDomain = true };
            app.MapHubs(config);
        }
    }
}
