using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClient
{
    static class Program
    {
        public static IHubProxy hub;
        public static HubConnection connection;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Form2());

            Process[] pname = Process.GetProcessesByName("ChatHost");
            if (pname.Length == 0)
                Process.Start("ChatHost.exe");


            string url = @"http://localhost:8080/";
            connection = new HubConnection(url);
            hub = connection.CreateHubProxy("ChatHub");
            connection.Start().Wait();
            string conId = connection.ConnectionId;
           
            Application.Run(new frmRegister(conId));

        }
    }
}
