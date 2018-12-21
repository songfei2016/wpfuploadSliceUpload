using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using Microsoft.Practices.Prism.Commands;
using Newtonsoft.Json;
using St.Model;
using St.Common;
using St.Upload.Service;
using Serilog;
using St.Upload.View;
using System.Threading;
using System.ComponentModel;
using St.Common.Manager;

namespace St.Upload.ViewModel
{
    public class UploadViewModel : ViewModelBase
    {

        #region PrivateField

        private int _resourceId;
        private double _progressRate;
        private string _resourceName;
        private string _filePath;
        private string _fileName;
        private string _fileUrl;
        private string _uploadStatus;
        private readonly TaskScheduler _syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        private readonly UploadService _uploadService;
        private readonly IApiService _apiService;

        private readonly string _token = GlobalData.Instance.UploadToken;
        private const int Buffersize = 1024 * 512;
        private readonly ResourceUpload _view;
        private const string DefaultRange = "0-0";
        private int _reTryUploadTimes = 0;
        private const int DefaultReTryUploadTimes = 4;
        private bool _isClosed = false;

        private bool _isReUpload;

        private readonly object locker = new object();
        readonly AsyncLock m_lock = new AsyncLock();

        #endregion

        #region Field

        public string UploadStatus
        {
            get { return _uploadStatus; }
            set { SetProperty(ref _uploadStatus, value); }
        }

        public int ReSourceId
        {
            get { return _resourceId; }
            set { SetProperty(ref _resourceId, value); }
        }

        public string ReSourceName
        {
            get { return _resourceName; }
            set { SetProperty(ref _resourceName, value); }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }

        public string FileUrl
        {
            get { return _fileUrl; }
            set { SetProperty(ref _fileUrl, value); }
        }

        public double ProgressRate
        {
            get { return _progressRate; }
            set { SetProperty(ref _progressRate, value); }
        }

        #endregion

        #region Ctor

        public UploadViewModel(int id, bool reUpload, ResourceUpload view)
        {
            _view = view;
            _resourceId = id;
            _isReUpload = reUpload;
            _uploadService = new UploadService();
            _apiService = new ApiService();
            LoadCommand = DelegateCommand.FromAsyncHandler(LoadAsync);
            // StartUpload = new DelegateCommand(StarUploadAsync);
            StartUpload = DelegateCommand.FromAsyncHandler(StarUploadAsync);
            SelectFile = new DelegateCommand(SelectFiles);
            view.Closing += Exit;

        }


        #endregion

        #region Method

        private async Task LoadAsync()
        {
            var result = await _apiService.GetResource(GlobalData.Instance.AccessToken, ReSourceId);
            if (result.Status == "0")
            {
                var resourceModel = JsonConvert.DeserializeObject<ResourceModel>(result.Data.ToString());
                ReSourceId = resourceModel.Id;
                ReSourceName = resourceModel.Name;
                FileUrl = resourceModel.Path;
                UploadStatus = resourceModel.UploadStatus ? "已上传" : "未上传";
                //查询该资源上次未传完的记录
                var lastRecord = _uploadService.GetLastUploadRecord(ReSourceId);
                if (lastRecord != null)
                {
                    FilePath = lastRecord.FilePath;
                    FileName = Path.GetFileName(FilePath);
                    _view.BtnUpload.Content = "继  续";
                }

            }
        }

        private async Task StarUploadAsync()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                HasErrorMsg("-1", Messages.WarningSelectFileFirst);
                return;
            }
            _reTryUploadTimes = 0;
            _view.BtnSelectFile.IsEnabled = false;
            _view.BtnUpload.IsEnabled = false;
            using (var releaser = await m_lock.LockAsync())
            {
                await Task.Factory.StartNew(() => RunUpload(FileName, FilePath));
            }

        }

        private async Task RunUpload(string fileName, string path)
        {
            UploadStatus = "上传中…";
            try
            {
                var applyResult = _uploadService.ApplyUpload(fileName, _token);
                if (applyResult.Code != "0") throw new Exception("申请上传失败！");
                var fileInfo = FileService.GetFileLength(path);
                var uploadPara = new DoUploadPara()
                {
                    Token = _token,
                    Host = "http://" + applyResult.Host,
                    Range = DefaultRange,
                    Cid = applyResult.Cid,
                    Method = "write"
                };
                //如果是重新传的，则把本地续传记录清除
                if (_isReUpload)
                {
                    _uploadService.DeleteUploadDataFromLocal(ReSourceId);
                    _isReUpload = false;
                }

                //获取上次断点的range记录
                var lastRecord = _uploadService.GetLastUploadRecord(ReSourceId);
                if (lastRecord != null && lastRecord.Status != "1")
                {
                    uploadPara.Range = lastRecord.Range == "" ? DefaultRange : lastRecord.Range;
                    uploadPara.Cid = lastRecord.Cid;
                }

                try
                {
                    FileUrl = string.Empty;
                    DoUploadResult uploadResult;
                    UploadLocalDataModel uploadData;
                    do
                    {
                        if (_isClosed)
                        {
                            throw new Exception("窗口关闭导致停止上传。");
                        }
                        var lastEnd = Convert.ToInt64(uploadPara.Range.Split('-')[1]);
                        if (lastEnd >= fileInfo.FileLength)
                        {
                            ProgressRate = 100;
                            break;
                        }
                        //计算本次Range
                        var percent = (double)lastEnd / fileInfo.FileLength;
                        var end = lastEnd + Buffersize;
                        ProgressRate = Math.Round(percent, 3) * 100;
                        uploadPara.Range = $"{lastEnd}-{end}";
                        //执行上传
                        uploadResult = await _uploadService.DoUpload(ref uploadPara, path, fileName);
                        if (uploadResult.Code == "-2998")
                        {
                            _uploadService.DeleteUploadDataFromLocal(ReSourceId);
                            throw new Exception("上传Cid失效，删除本地记录，下次重新上传！");
                        }
                        if (uploadResult.Code != "0") throw new Exception();
                        uploadData = new UploadLocalDataModel()
                        {
                            Key = fileInfo.FileHashCode,
                            DateTime = DateTime.Now,
                            Path = string.Empty,
                            Range = uploadPara.Range,
                            Status = "0",
                            ResourceId = ReSourceId,
                            Cid = uploadPara.Cid,
                            FilePath = FilePath

                        };
                        _uploadService.SaveUploadDataToLocal(uploadData);

                    } while (uploadResult.Code != "-9999");
                    //上传完成
                    var finishResultModel = _uploadService.FinishUpload(uploadPara.Host, uploadPara.Cid,
                        uploadPara.Token, "test");
                    //通知api上传完成
                    var updateReuslt =
                        await _uploadService.UpdateResource(GlobalData.Instance.AccessToken, ReSourceId, finishResultModel.Url);
                    if (updateReuslt)
                    {
                        FileUrl = finishResultModel.Url;
                        UploadStatus = "已完成";
                    }
                    else
                    {
                        Log.Logger.Error($"资源{ReSourceId}更新文件地址失败,地址：{finishResultModel.Url}");
                    }
                    uploadData = new UploadLocalDataModel()
                    {
                        Key = fileInfo.FileHashCode,
                        DateTime = DateTime.Now,
                        Path = finishResultModel.Url,
                        Range = uploadPara.Range,
                        Status = "1",
                        ResourceId = ReSourceId,
                        Cid = uploadPara.Cid
                    };
                    _uploadService.SaveUploadDataToLocal(uploadData);

                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"资源{ReSourceId}上传异常,当前Range{uploadPara.Range},异常信息：{ex.Message}");
                    HasErrorMsg("-1", "上传中断，请点击继续！");
                    ResetButton(true, "继  续");
                    UploadStatus = "已中断";
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"资源{ReSourceId}上传异常,异常信息：{ex.Message}");
                HasErrorMsg("-1", "上传中断，请点击继续！");
                ResetButton(true, "继  续");
                UploadStatus = "已中断";
            }

        }

        private void SelectFiles()
        {
            var file = new OpenFileDialog();
            file.ShowDialog();
            FileName = Path.GetFileName(file.FileName);
            FilePath = file.FileName;
        }

        public void ResetButton(bool status, string btnContent = "")
        {
            Task.Factory.StartNew(() => SetButton(status, btnContent),
             new CancellationTokenSource().Token, TaskCreationOptions.None, _syncContextTaskScheduler).Wait();
        }

        private void SetButton(bool status, string btnContent)
        {
            _view.BtnSelectFile.IsEnabled = true;
            _view.BtnUpload.IsEnabled = true;
            if (btnContent != "") _view.BtnUpload.Content = btnContent;
        }

        private void Exit(object sender, CancelEventArgs e)
        {
            _isClosed = true;
        }

        #endregion

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand StartUpload { get; set; }
        public ICommand SelectFile { get; set; }

        #endregion


    }
}
