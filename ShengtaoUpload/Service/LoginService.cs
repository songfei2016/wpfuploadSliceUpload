using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using St.Model;
using Newtonsoft.Json;
using Serilog;
using St.Common;
using St.Common.Manager;

namespace St.Upload.Service
{
    public class LoginService
    {

        #region Field

        private readonly HttpManager _httpManager;
        private readonly IApiService _apiService;

        #endregion

        #region ctor
        public LoginService()
        {
            _httpManager = new HttpManager();
            _apiService = new ApiService();
        }

        #endregion

        #region Method
        public async Task<ResponseResult> GetToken(string userName, string password)
        {
            var result = await _apiService.GetToken(userName, password);
            return result;
        }
        public async Task<bool> CheckAuth(string token)
        {
            var result = await _apiService.Check(token);
            if (result.Status != "0") return result.Status == "0";
            var resultData = JsonConvert.DeserializeObject<CheckResultModel>(result.Data.ToString());
            GlobalData.Instance.UploadToken = resultData.Token;
            GlobalData.Instance.FileUploadUrl = resultData.FileUploadUrl.StartsWith("http:") ? resultData.FileUploadUrl : $"http://{resultData.FileUploadUrl}";
            return result.Status == "0";
        }


        public LocalUserModel GetLocalUserInfo()
        {
            var userString = File.ReadAllText(GlobalResources.LocalUserInfoPath, Encoding.UTF8);
            return string.IsNullOrEmpty(userString) ? new LocalUserModel() : JsonConvert.DeserializeObject<LocalUserModel>(userString);
        }

        public void SaveLocalUserInfo(LocalUserModel userInfo)
        {
            File.WriteAllText(GlobalResources.LocalUserInfoPath, JsonConvert.SerializeObject(userInfo), Encoding.UTF8);
        }

        public async Task WriteConfigAsync(string username, string password, bool autologin)
        {
            await Task.Run(() =>
            {
                try
                {
                    var userInfo = new LocalUserModel()
                    {
                        UserName = username,
                        Password = password,
                        IsRemember = true,
                        AutoLogin = autologin
                    };
                    var json = JsonConvert.SerializeObject(userInfo,
                        Formatting.Indented);

                    File.WriteAllText(GlobalResources.LocalUserInfoPath, json, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"【write config exception】：{ex}");
                }
            });
        }

        #endregion


    }
}
