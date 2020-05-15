using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://localhost:8080/";
            using (WebApp.Start<Startup>(url))
            {
                // Make long polling connections wait a maximum of 110 seconds for a
                // response. When that time expires, trigger a timeout command and
                // make the client reconnect.
                GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);

                // Wait a maximum of 30 seconds after a transport connection is lost
                // before raising the Disconnected event to terminate the SignalR connection.
                GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(10);

                // For transports other than long polling, send a keepalive packet every
                // 10 seconds. 
                // This value must be no more than 1/3 of the DisconnectTimeout value.
                GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(3);

                Console.WriteLine(string.Format("Server running at {0}", url));
                Console.ReadLine();
            }
        }
    }
}
