using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using St.Model;
using St.Common;
using St.Upload.Service;
using St.Upload.View;
using System.Windows;
using System.ComponentModel;

namespace St.Upload.ViewModel
{
    public class ResourceViewModel : ViewModelBase
    {
        #region Field

        private readonly IApiService _apiService;
        private ResourceModel _selectedResource;
        private int _pageIndex;
        private int _totalPage;
        private const int PageSize = 10;
        private int _currentPageIndex;

        private string _sourceName;
        private MainWindow _mianview;

        private static string _sortFiled;
        private static string _sortByType;


        #endregion

        #region Properties
        public ObservableCollection<ResourceModel> ResourceList { get; set; }
        public ResourceModel SelectedResource
        {
            get { return _selectedResource; }
            set { SetProperty(ref _selectedResource, value); }
        }



        public int PageIndex
        {
            get { return _pageIndex; }
            set { SetProperty(ref _pageIndex, value); }
        }

        public int TotalPage
        {
            get { return _totalPage; }
            set { SetProperty(ref _totalPage, value); }
        }

        public string SourceName
        {
            get { return _sourceName; }
            set { SetProperty(ref _sourceName, value); }
        }

        #endregion

        #region Ctor
        public ResourceViewModel(MainWindow view, string sortFiled = "", string sortType = "")
        {
            _mianview = view;
            _apiService = new ApiService();
            ResourceList = new ObservableCollection<ResourceModel>();
            LoadCommand = DelegateCommand.FromAsyncHandler(LoadAsync);
            PrePageCommand = DelegateCommand.FromAsyncHandler(PrePageAsync);
            NextPageCommand = DelegateCommand.FromAsyncHandler(NextPageAsync);
            GoToPageCommand = DelegateCommand.FromAsyncHandler(GoToPageAsync);
            RefreshCommand = DelegateCommand.FromAsyncHandler(RefreshAsync);
            QueryCommand = DelegateCommand.FromAsyncHandler(QueryAsync);
            //SortingCommand = DelegateCommand.FromAsyncHandler(SortingAsync);
            if (!string.IsNullOrEmpty(sortFiled))
            {
                SortingAsync(sortFiled);
            }
            view.Closing += Exit;
        }



        #endregion

        #region Method

        private void Exit(object sender, CancelEventArgs e)
        {
            Environment.Exit(System.Environment.ExitCode);
        }
        private async Task LoadAsync()
        {
            await GetResource();
        }

        private async Task GetResource(int pageIndex = 1, int pageSize = PageSize, string sortBy = "", string sortByType = "")
        {
            ResourceList.Clear();
            var result = await _apiService.GetResourcePaged(GlobalData.Instance.AccessToken, pageIndex, pageSize, SourceName, sortBy, sortByType);
            if (result != null && result.Total > 0)
            {
                ResourceList.Clear();
                result.Items.ForEach(r =>
                {
                    var recource = new ResourceModel
                    {
                        Id = r.Id,
                        AddTime = r.AddTime,
                        Description = r.Description,
                        FileName = r.FileName,
                        Name = r.Name,
                        Path = r.Path,
                        SchoolId = r.SchoolId,
                        UploadStatus = r.UploadStatus,
                        UploadTime = r.UploadTime,
                        UserId = r.UserId,
                        GotoUpload = new DelegateCommand(ShowUpload),
                        GotoDetail = new DelegateCommand(ShowDetail),
                        ReGotoUpload = new DelegateCommand(ShowReUpload)
                        //SortingCommand = DelegateCommand.FromAsyncHandler(SortingAsync)

                    };

                    ResourceList.Add(recource);
                });
                TotalPage = Convert.ToInt32(Math.Ceiling((double)result.Total / pageSize));
            }
            PageIndex = pageIndex;
            _currentPageIndex = pageIndex;
        }

        private void SortingAsync(string sortFiled)
        {
            if (sortFiled != _sortFiled)
            {
                _sortByType = "asc";
            }
            else
            {
                switch (_sortByType)
                {
                    case "":
                        _sortByType = "asc";
                        break;
                    case "asc":
                        _sortByType = "desc";
                        break;
                    case "desc":
                        _sortByType = "asc";
                        break;
                }
            }
            _sortFiled = sortFiled;
            GetResource(1, PageSize, _sortFiled, _sortByType);
        }

        private async Task RefreshAsync()
        {
            await GetResource(_currentPageIndex);
        }

        private async Task QueryAsync()
        {
            await GetResource(1);
        }

        private async Task PrePageAsync()
        {
            var cpage = 1;
            if (_currentPageIndex - 1 > 0)
                cpage = _pageIndex - 1;
            await GetResource(cpage);
        }

        private async Task NextPageAsync()
        {
            var cpage = _currentPageIndex + 1 > _totalPage ? _totalPage : _currentPageIndex + 1;
            await GetResource(cpage);
        }

        private async Task GoToPageAsync()
        {
            var cpage = PageIndex;
            if (PageIndex >= TotalPage)
                cpage = TotalPage;
            await GetResource(cpage);
        }

        private void ShowUpload()
        {
            var uploadView = new ResourceUpload(SelectedResource.Id);
            uploadView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            uploadView.ShowDialog();
        }
        private void ShowReUpload()
        {
            var uploadView = new ResourceUpload(SelectedResource.Id, true)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            uploadView.ShowDialog();
        }

        private void ShowDetail()
        {
            var detailView = new ResourceDetail(SelectedResource.Id);
            detailView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            detailView.ShowDialog();
        }

        #endregion

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand PrePageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand GoToPageCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ICommand QueryCommand { get; set; }


        #endregion

        #region Event

        public void SortByHeader(object sender, RoutedEventArgs e)
        {
            var t = DateTime.Now;
        }
        #endregion

    }
}
