using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class frmRegister : Form
    {
        string connectionId;
        public frmRegister(string conId)
        {


            InitializeComponent();
            connectionId = conId;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtuserName.Text))
            {
                string imagePath = ""; 
                if (picProfileImage.BackgroundImage != null)
                {
                    ChatHost.Service service = new ChatHost.Service();
                    imagePath = service.UploadImage(picProfileImage.BackgroundImage);

                }
                string id = Guid.NewGuid().ToString("N");
                string name = txtuserName.Text.Trim();
                Task t = Program.hub.Invoke("RegisterUser", connectionId, id, name, imagePath);
                t.Wait();
                if (t.Status == TaskStatus.RanToCompletion)
                {
                   
                    Form2 form1 = new Form2(connectionId, id, name, imagePath);
                    this.Hide();
                    form1.Show();
                }

                //Form1 form2 = new Form1();
                //this.Hide();
                //form2.Show();
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File (*.jpg,*.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                picProfileImage.BackgroundImage = Image.FromFile(fileName);
            }
        }

    }
}
