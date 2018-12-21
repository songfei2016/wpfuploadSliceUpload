using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using St.Model;
using Newtonsoft.Json;
using Prism.Commands;
using Serilog;
using St.Common;
using St.Upload.Service;


namespace St.Upload.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {

        #region field

        //private fields
        private readonly Window _loginView;
        private bool _autoLogin;
        private bool _isLoginEnabled = true;
        private string _password;
        private bool _rememberMe;
        private bool _showProgressBar;

        private readonly LoginService _loginService;
        //properties
        private string _userName;

        //public fields
        public bool IsLoginSucceeded;



        #endregion

        #region properties

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public bool IsLoginEnabled
        {
            get { return _isLoginEnabled; }
            set { SetProperty(ref _isLoginEnabled, value); }
        }

        public bool ShowProgressBar
        {
            get { return _showProgressBar; }
            set { SetProperty(ref _showProgressBar, value); }
        }

        public bool RememberMe
        {
            get { return _rememberMe; }
            set
            {
                if (SetProperty(ref _rememberMe, value) && !value)
                    AutoLogin = false;
            }
        }

        public bool AutoLogin
        {
            get { return _autoLogin; }
            set
            {
                if (SetProperty(ref _autoLogin, value) && value)
                    RememberMe = true;
            }
        }

        private Visibility _settingVisibility = Visibility.Collapsed;
        public Visibility SettingVisibility
        {
            get { return _settingVisibility; }
            set { SetProperty(ref _settingVisibility, value); }
        }

        #endregion

        #region ctor

        public LoginViewModel(Window loginView)
        {
            _loginView = loginView;
            _loginView.Closing += _loginView_Closing;
            _loginService = new LoginService();
            LoginCommand = DelegateCommand.FromAsyncHandler(LoginAsync);
            LoadCommand = DelegateCommand.FromAsyncHandler(LoadAsync);
            TopMostTriggerCommand = new DelegateCommand(TriggerTopMost);
        }

        #endregion

        #region commands

        //commands
        public ICommand LoginCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand SaveSettingCommand { get; set; }
        public ICommand ShowSettingCommand { get; set; }

        public ICommand TopMostTriggerCommand { get; set; }
        public ICommand ShowLogCommand { get; set; }

        #endregion

        #region  handlers
        //command handlers
        private async Task LoginAsync()
        {
            ResetStatus();

            if (!ValidateInputs())
                return;

            await Login();

        }

        private async Task LoadAsync()
        {
            try
            {
                var localUser = _loginService.GetLocalUserInfo();
                AutoLogin = localUser.AutoLogin;
                UserName = localUser.UserName;
                Password = localUser.Password;
                RememberMe = localUser.IsRemember;
                if (AutoLogin)
                    await Login();
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException?.Message.Replace("\r\n", string.Empty) ??
                              ex.Message.Replace("\r\n", string.Empty);
                HasErrorMsg("-1", errorMsg);
            }
        }


        //event handlers
        private void _loginView_Closing(object sender, CancelEventArgs e)
        {
            //if (IsLoginSucceeded)
            //{
            //    var mainView = DependencyResolver.Current.Container.Resolve<MainView>();
            //    if (mainView.IsLoaded)
            //    {
            //        Log.Logger.Debug("【login succeeded, make loaded main view visible】");
            //        mainView.Visibility = Visibility.Visible;

            //        var regionManager = DependencyResolver.Current.Container.Resolve<IRegionManager>();

            //        foreach (var activeView in regionManager.Regions[RegionNames.ContentRegion].ActiveViews)
            //        {
            //            if (activeView.ToString().Contains(GlobalResources.CollaborativeInfoContentView))
            //            {
            //                Log.Logger.Debug($"【reload default view】：{GlobalResources.CollaborativeInfoContentView}");
            //                var regionView = activeView as UserControl;

            //                if (regionView != null)
            //                {
            //                    var reloadRegionService = regionView.DataContext as IReloadRegion;

            //                    reloadRegionService?.ReloadAsync();
            //                }
            //            }
            //            else
            //            {
            //                Log.Logger.Debug(
            //                    $"【navigate to default view】：{GlobalResources.CollaborativeInfoContentView}");

            //                regionManager.RequestNavigate(RegionNames.ContentRegion,
            //                    new Uri(GlobalResources.CollaborativeInfoContentView, UriKind.Relative));
            //            }
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    Application.Current.Shutdown();
            //}
        }

        #endregion

        #region methods
        //methods
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                HasErrorMsg("-1", Messages.WarningUserNameEmpty);
                return false;
            }

            if (!string.IsNullOrEmpty(Password)) return true;
            HasErrorMsg("-1", Messages.WarningPasswordEmpty);
            return false;
        }

        private void ResetStatus()
        {
            ShowProgressBar = false;
        }

        private void SetBeginLoginStatus()
        {
            IsLoginEnabled = false;
            ShowProgressBar = true;
        }

        private void SetEndLoginStatus()
        {
            IsLoginEnabled = true;
            ShowProgressBar = false;
        }

        private async Task Login()
        {
            SetBeginLoginStatus();

            //logic for login
            try
            {
                var succeeded = await AuthenticateUserAsync();
                if (succeeded)
                {
                    IsLoginSucceeded = true;
                    await _loginView.Dispatcher.BeginInvoke(new Action(() => { _loginView.Close(); }));
                    if (RememberMe)
                    {
                        var userInfo = new LocalUserModel()
                        {
                            UserName = UserName,
                            Password = Password,
                            AutoLogin = AutoLogin,
                            IsRemember = RememberMe
                        };
                        _loginService.SaveLocalUserInfo(userInfo);
                    }

                }
            }
            catch (Exception ex)
            {
                HasErrorMsg("-1", ex.InnerException?.Message.Replace("\r\n", "") ?? ex.Message.Replace("\r\n", ""));
            }
            finally
            {
                SetEndLoginStatus();
            }
        }

        private async Task<bool> AuthenticateUserAsync()
        {
            Log.Logger.Debug("【**********************************************************************************】");
            Log.Logger.Debug($"【login step1】：apply for token begins");
            var reuslt = await _loginService.GetToken(UserName, Password);

            Log.Logger.Debug($"【login step1】：apply for token ends");

            if (HasErrorMsg(reuslt.Status, reuslt.Message))
            {
                return false;
            }
            Log.Logger.Debug($"【login step2】：set token begins");
            if (reuslt.Status == "0")
            {
                GlobalData.Instance.AccessToken = JsonConvert.DeserializeObject<TokenData>(reuslt.Data.ToString())?.AccessToken;
                var authResult = await _loginService.CheckAuth(GlobalData.Instance.AccessToken);
                if (authResult) return true;
                HasErrorMsg("-1", Messages.ErrorLoginFailed);
                return false;
            }
            //Log.Logger.Error($"【get user info via account failed】：user info is null");
            HasErrorMsg("-1", Messages.ErrorLoginFailed);
            return false;
        }

        private void TriggerTopMost()
        {
            _loginView.Topmost = !_loginView.Topmost;
        }


        #endregion

    }
}