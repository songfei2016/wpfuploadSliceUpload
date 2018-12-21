using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St.Upload.ViewModel
{
    public class FileModel
    {
        public long FileLength { get; set; }
        public string FileHashCode { get; set; }

        public byte[] FileData { get; set; }


    }
}
