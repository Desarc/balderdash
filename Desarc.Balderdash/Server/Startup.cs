﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Owin;

[assembly: OwinStartup(typeof(Desarc.Balderdash.Server.Startup))]

namespace Desarc.Balderdash.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map(
                "/signalr",
                map =>
                {
                    map.UseCors(CorsOptions.AllowAll);
                    map.RunSignalR(new HubConfiguration());
                });
        }
    }
}
