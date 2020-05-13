using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace WebFormFileUploader
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (inFileUpload.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(inFileUpload.FileName);
                    inFileUpload.SaveAs(Server.MapPath("~/") + filename);
                    StatusLabel.Text = "Upload status: File uploaded!";
                    StatusLabel.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    StatusLabel.ForeColor = Color.Red;
                }
            }
        }
    }
}