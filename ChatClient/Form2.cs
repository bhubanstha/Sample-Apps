using ChatClient.Model;
using ChatHost;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form2 : Form
    {
        private readonly string userId;
        private static string receiverId;
        private readonly string myImage;
        private List<Attachment> FileAttachment;
        private List<string> FileNames;
        public Form2(string conId, string myId, string name, string imagePath)
        {
            InitializeComponent();
            myImage = imagePath;
            userId = myId;
            this.Text += " " + name;
            Screen screen = Screen.FromControl(this);
            this.MinimumSize = new Size(screen.WorkingArea.Width / 2, screen.WorkingArea.Height);
            Program.hub.On<string, string, List<string>>("ReciveMessage", (x, y, z) => ReciveMessage(x, y, z));
            Program.hub.On("CheckNewUser", x => CheckUser());
            Program.hub.On("UserList", x => UserList(x));
            Program.hub.On("CloseApp", x => CloseApp());
            Program.hub.On("UserDisconnected", x => UserDisconnected(x));
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            FileAttachment = new List<Attachment>();
            FileNames = new List<string>();
            pnlMessage.AutoScroll = false;

            pnlMessage.HorizontalScroll.Enabled = false;
            pnlMessage.HorizontalScroll.Visible = false;
            pnlMessage.HorizontalScroll.Maximum = 0;
            pnlMessage.AutoScroll = true;

            ctrlTyping.OnAttachmentClicked += CtrlTyping_OnAttachmentClicked;
            Program.hub.Invoke("GetAllUser", userId);
        }

        private void CtrlTyping_OnAttachmentClicked(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                using (FileStream fs = File.OpenRead(fileName))
                {
                    byte[] fileByte = new byte[fs.Length];
                    fs.Read(fileByte, 0, fileByte.Length);
                    Attachment a = new Attachment
                    {
                        FileArray = fileByte,
                        Extension = Path.GetExtension(fileName)
                    };
                    FileAttachment.Add(a);
                }

            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ctrlTyping_OnHitEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(receiverId))
            {
                string msg = ctrlTyping.Value;
                if (!string.IsNullOrEmpty(msg))
                {

                    if (FileAttachment.Count > 0)
                    {
                        ChatHost.Service service = new Service();
                        foreach (Attachment item in FileAttachment)
                        {
                            string fileName = service.UploadFile(item.FileArray, item.Extension);
                            FileNames.Add(fileName);
                        }
                    }

                    chat.MeBubble mb = new chat.MeBubble();
                    mb.Attachment = FileNames;
                    mb.Body = msg;
                    mb.UserImage = Image.FromFile(myImage);
                    mb.Dock = DockStyle.Bottom;

                    pnlMessage.Controls.Add(mb);
                    try
                    {
                        Program.hub.Invoke("SendMessage", receiverId, msg, myImage, FileNames).Wait();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                    finally
                    {
                        ctrlTyping.Value = "";
                        pnlMessage.VerticalScroll.Value = pnlMessage.VerticalScroll.Maximum;
                        FileNames.Clear();
                    }
                }

            }
            else
            {
                MessageBox.Show("select message receiver");
            }

        }

        private void Uc_OnClick(object sender, EventArgs e)
        {
            chat.Users u = (chat.Users)sender;
            u.BackColor = Color.NavajoWhite;
            receiverId = u.Name;
        }

        private void UserList(string userList)
        {

            int i = 0;
            List<User> users = JsonConvert.DeserializeObject<List<User>>(userList);
            users = users.Where(x => x.UserId != userId).ToList();
            this.BeginInvoke(new MethodInvoker(delegate
            {
                pnlUserList.Controls.Clear();

                foreach (User u in users)
                {
                    chat.Users uc = new chat.Users();
                    uc.Username = u.Name;
                    uc.Name = u.UserId;
                    uc.UserImage = Image.FromFile(u.ImagePath);
                    uc.UserStatus = u.Status==OnlineStatus.Online? chat.Status.Online : (u.Status==OnlineStatus.Offline? chat.Status.Offline: chat.Status.Away);
                    uc.Dock = DockStyle.Top;
                    uc.OnClick += Uc_OnClick;
                    pnlUserList.Controls.Add(uc);
                }
            }));

        }
        private void CheckUser()
        {
            Program.hub.Invoke("GetAllUser", userId).Wait();
        }
        private void ReciveMessage(string msg, string img, List<string> attachments)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                //rtbMessage.Text = msg + Environment.NewLine;
                chat.YouBubble yb = new chat.YouBubble();
                yb.Body = msg;
                yb.Attachment = attachments;
                yb.UserImage = Image.FromFile(img);
                yb.Dock = DockStyle.Bottom;
                pnlMessage.Controls.Add(yb);

            }));
        }
        private void CloseApp()
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                Application.Exit();
            }));
        }

        private void UserDisconnected(string id)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                chat.Users u = (chat.Users)pnlUserList.Controls.Find(id, true).FirstOrDefault();
                u.UserStatus = chat.Status.Offline;

            }));

        }


    }
}