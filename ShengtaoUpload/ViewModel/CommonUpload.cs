namespace St.Upload.ViewModel
{
    /// <summary>
    /// 普通上传参数
    /// </summary>
    public class CommonUpload
    {
        public string PostForm { get; set; }
        public string FileUploadUrl { get; set; }
        public string Token { get; set; }
        public string Creator { get; set; }
        public string Context { get; set; }
        public string Callback { get; set; }
        public string FileName { get; set; }
    }
}
