using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using St.Model;
using Newtonsoft.Json;
using St.Common;
using St.Upload.Service;


namespace St.Upload.ViewModel
{
    public class ResourceDetailViewModel : ViewModelBase
    {
        #region Filed

        private int _id;
        private string _name;
        private DateTime _addTime;
        private DateTime _uploadTime;
        private string _filePath;
        private string _description;
        private string _status;

        private readonly IApiService _apiService;

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DateTime AddTime
        {
            get { return _addTime; }
            set { SetProperty(ref _addTime, value); }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public DateTime UploadTime
        {
            get { return _uploadTime; }
            set { SetProperty(ref _uploadTime, value); }
        }


        #endregion

        #region Ctor
        public ResourceDetailViewModel(int id)
        {
            _apiService = new ApiService();
            Id = id;
            LoadCommand = DelegateCommand.FromAsyncHandler(LoadAsync);
        }

        #endregion

        #region Method

        private async Task LoadAsync()
        {
            var result = await _apiService.GetResource(GlobalData.Instance.AccessToken, Id);
            if (result.Status == "0")
            {
                var resource = JsonConvert.DeserializeObject<ResourceModel>(result.Data.ToString());
                Id = resource.Id;
                Name = resource.Name;
                FilePath = resource.Path;
                Description = resource.Description;
                Status = resource.UploadStatus ? "Check" : "Close";
                UploadTime = resource.UploadTime ?? new DateTime();
                AddTime = resource.AddTime;
            }
        }


        #endregion

        #region Command

        public ICommand LoadCommand { get; set; }

        #endregion


    }
}
