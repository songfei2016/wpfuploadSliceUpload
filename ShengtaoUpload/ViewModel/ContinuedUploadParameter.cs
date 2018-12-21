using St.Model;

namespace St.Upload.ViewModel
{

    public class ApplyUploadResult : BaseResult
    {
        public string Cid { get; set; }
        public string Host { get; set; }
    }

    public class DoUploadPara
    {
        public byte[] Data { get; set; }
        public string Host { get; set; }
        public string Method { get; set; }
        public string Cid { get; set; }
        public string Range { get; set; }
        public string Token { get; set; }
    }


    public class DoUploadResult : BaseResult
    {
        public string Range { get; set; }
    }

    public class FinishUploadResult : BaseResult
    {
        public string Url { get; set; }
    }

}
