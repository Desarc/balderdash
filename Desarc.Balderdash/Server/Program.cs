using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Owin.Hosting;

namespace Desarc.Balderdash.Server
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            const string UrlWebServer = "http://*:1337/";

            using (WebApp.Start<Startup>(UrlWebServer))
            {
                Console.WriteLine("{0} running at {1}", Assembly.GetExecutingAssembly().GetName().Name, UrlWebServer);
                Console.ReadLine();
            }


            Console.WriteLine("Server stopped.");
        }
    }
}