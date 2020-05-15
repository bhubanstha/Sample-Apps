using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChatHost
{
    
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        private string connectionId;
        public void SendMessage(string toUser, string message, string senderImage, List<string> attachments)
        {
            //IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            string conId = User.UserList.Where(x => x.UserId == toUser).FirstOrDefault().ConnectionId;
            //Clients.User(conId).ReciveMessage(message);
            //Clients.Caller.ReciveMessage(message);
            Clients.Client(conId).ReciveMessage(message, senderImage, attachments);

            //Clients.All.ReciveMessage(test);
        }

        public void GetAllUser(string userId)
        {
            //List<User> u = User.UserList.Where(x => x.UserId != userId).ToList();
            string userList = JsonConvert.SerializeObject(User.UserList);
            //Clients.Others.UserList(userList);
            Clients.All.UserList(userList);           
        }

        

        public void RegisterUser(string conId, string userId, string userName, string imagePath)
        {
            User.AddUser(conId, userId, userName, imagePath);
            Clients.All.CheckNewUser();
        }

        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            Console.WriteLine("Connection created: " + id);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            Console.WriteLine("Connection closed for: " + id);
            User user = User.UserList.Where(x => x.ConnectionId == id).FirstOrDefault();
            user.Status = OnlineStatus.Offline;

            Clients.All.UserDisconnected(user.UserId);
            return base.OnDisconnected(stopCalled);
        }
    }
}
