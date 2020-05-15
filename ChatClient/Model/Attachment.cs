using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Model
{
    public class Attachment
    {
        public byte[] FileArray { get; set; }
        public string Extension { get; set; }
    }
}
