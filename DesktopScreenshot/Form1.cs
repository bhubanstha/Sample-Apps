using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopScreenshot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            picImage.BackgroundImage = Image.FromFile( @"E:\photos\Dashain 2076\BHU_2483.JPG");
            picImage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            ScreenCapture sc = new ScreenCapture();

            //string fullpath = filepath + "\\" + filename;

            //sc.CaptureWindowToFile(this.Handle, fullpath, format);
            Image img =  sc.CaptureScreen();
            picImage.BackgroundImage = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Cursor = System.Windows.Forms.Cursors.Cross;
            frma frma = new frma();
            frma.Opacity = 0;//  0.2D;
            frma.Cursor = Cursors.Cross;
            
            frma.WindowState = FormWindowState.Maximized;
            frma.Show();
            //this.Cursor = Cursor.
            //frmAreaSelection frmAreaSelection = new frmAreaSelection();
            //frmAreaSelection.Show();
        }
    }
}
