using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Desarc.Balderdash.Server
{
    public class BalderdashHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}