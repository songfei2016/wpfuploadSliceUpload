using System;
using System.IO;
using St.Common;
using St.Upload.ViewModel;


namespace St.Upload
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }
    }
}
