using St.Upload.ViewModel;

namespace St.Upload.View
{
    /// <summary>
    /// ResourceUpload.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceUpload
    {
        //private int id;

        public ResourceUpload(int id, bool reUpload = false)
        {
            InitializeComponent();
            DataContext = new UploadViewModel(id, reUpload, this);
        }
    }
}
