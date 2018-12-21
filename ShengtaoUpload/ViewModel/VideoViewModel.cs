namespace St.Upload.ViewModel
{
    public class VideoViewModel
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string PhysicalPath { get; set; }
    }

    public class UploadFile
    {
        public UploadFile()
        {
            ContentType = "application/octet-stream";
        }
        public string Path { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
