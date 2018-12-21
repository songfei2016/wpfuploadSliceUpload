using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;
using St.Common;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using St.Upload.Service;
using St.Upload.ViewModel;

namespace St.Upload
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView 
    {

        //private const string HostUrl = "http://222.73.29.13:1210";
        private const string Token = "hcoasfqTMREK3lGauFl9Fw72i3MtsxFDtuoh2U-5f5hjaGVsZ2xjbmgxMA__";
        private const int Buffersieze = 1024 * 512;
        private readonly UploadService _uploadService = new UploadService();
        private readonly TaskScheduler _syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        public List<UploadViewModel> FilesList = new List<UploadViewModel>();
        public MainView()
        {
            InitializeComponent();
        }

        private void BtnSelectFile(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog file = new OpenFileDialog();
            //file.ShowDialog();
            //var videoView = new VideoViewModel
            //{
            //    PhysicalPath = file.FileName,
            //    FileName = file.FileName
            //};
            //this.FileList.DataContext = videoView;
            //FilesList.Add(new UploadViewModel()
            //{
            //    FileName = file.FileName
            //});
            //GridUploadFiles.DataContext = FilesList;
        }

        private void BtnStart(object sender, RoutedEventArgs e)
        {
            var path = txtName.Text;
            //var videoByte = FileService.SetImageToByteArray(path);
            var fileList = new List<UploadFile>();
            var fileName = System.IO.Path.GetFileName(path);
            // fileList.Add(new UploadFile { Filename = fileName, Path = path, Data = videoByte });

            //普通上传
            //var result = HttpService.HttpPostData(new Uri(_hostUrl + "/fileupload?token=" + _token), fileList);
            txtStartTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Task.Factory.StartNew(new Action(() =>
            {
                RunUpload(fileName, path);
            }));
        }

        #region 执行上传操作
        private async void RunUpload(string fileName, string path)
        {
            var applyResult = _uploadService.ApplyUpload(fileName, Token);
            if (applyResult.Code != "0") return;
            var uploadPara = new DoUploadPara()
            {
                Token = Token,
                Host = "http://" + applyResult.Host,
                Range = $"0-{Buffersieze}",
                Cid = applyResult.Cid,
                Method = "write"
            };
            DoUploadResult uploadResult;
            //do
            //{
            //    uploadResult = await _uploadService.DoUpload(ref uploadPara, path, fileName);
            //}
            //while (uploadResult.Code == "0");
            var finishResultModel = _uploadService.FinishUpload(uploadPara.Host, uploadPara.Cid, uploadPara.Token);
            Task.Factory.StartNew(() => UpdateUi(finishResultModel.Url),
                new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler).Wait();
        }

        private void UpdateUi(string url)
        {
            txtUrl.Text = url;
            txtEndTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
        #endregion

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}