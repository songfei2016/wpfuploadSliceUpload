using System.Windows.Controls;
using St.Upload.ViewModel;
using System.Windows;

namespace St.Upload
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ResourceViewModel(this);
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var d = sender as Button;
            var name = d.Name;
            DataContext = new ResourceViewModel(this, name);
        }
    }
}
