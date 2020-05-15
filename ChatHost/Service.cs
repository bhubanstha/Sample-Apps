using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ChatHost
{
    public class Service
    {
        string basePath = "";
        public Service()
        {
            basePath = AppDomain.CurrentDomain.BaseDirectory;
        }
        public string UploadImage(Image img )
        {
            if(img != null)
            {
                string path = Path.Combine(basePath, "UserProfile");
                string uniqeName = ReturnUniqueNameinDir(path, ".jpg");
                string fullPath = Path.Combine(path, uniqeName);
                img.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return fullPath;
            }
            return "";
        }

        public string UploadFile(byte[] fileContentArray, string extension)
        {
            string path = Path.Combine(basePath, "Attachment");
            string uniqeName = ReturnUniqueNameinDir(path, extension);
            string fullPath = Path.Combine(path, uniqeName);
            using (Stream fileStream = File.OpenWrite(fullPath))
            {
                fileStream.Write(fileContentArray, 0, fileContentArray.Length);
            }
            return fullPath;
        }

        private string ReturnUniqueNameinDir(string path, string ext)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string uniqeName = Guid.NewGuid().ToString("N") + ext;
            return uniqeName;
        }

    }
}
