using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatHost
{
    public class User
    {
        public  string UserId { get; set; }
        public  string Name { get; set; }
        public string ConnectionId { get; set; }
        public string ImagePath { get; set; }
        public OnlineStatus Status { get; set; }

        public static List<User> UserList;
        
        static User()
        {
            UserList = new List<User>();
        }
        public User()
        {

        }
        public User(string conId, string id, string name)
        {
            this.ConnectionId = conId;
            this.UserId = id;
            this.Name = name;
        }

        public User( string id, string name)
        {
            this.UserId = id;
            this.Name = name;

        }
        public static void AddUser(string conId, string id, string name, string imagePath)
        {
            UserList.Add(
                new User {ConnectionId=conId, UserId = id, Name = name, ImagePath = imagePath, Status = OnlineStatus.Online }
                );
        }
    }

    public enum OnlineStatus
    {
        Online,
        Offline,
        Away
    }
}
