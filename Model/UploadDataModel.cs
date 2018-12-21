using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St.Model
{
    /// <summary>
    /// 上传数据实体---用于记录断点续传json数据
    /// </summary>
    public class UploadLocalDataModel
    {
        public string Key { get; set; }
        public string Status { get; set; }
        public string Range { get; set; }
        public string Path { get; set; }
        public DateTime DateTime { get; set; }
        public int ResourceId { get; set; }
        public string Cid { get; set; }
        public string FilePath { get; set; }
    }
}
