using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using St.Model;
using Newtonsoft.Json;
using St.Common;
using St.Common.Manager;
using St.Upload.ViewModel;
using Newtonsoft.Json.Linq;

namespace St.Upload.Service
{
    public class ApiService : IApiService
    {
        #region Field

        private static readonly string Host = ConfigurationManager.AppSettings["apiHost"];
        private readonly HttpManager _httpManager = new HttpManager();

        #endregion

        #region Method
        public async Task<ResponseResult> GetToken(string userName, string password)
        {
            var url = Host + $"/api/token/gettoken?userName={userName.Replace(" ", "")}&password={password.Replace(" ", "")}&deviceId=platform";
            var result = await _httpManager.Request(url);
            return result;
        }

        public async Task<ResponseResult> Check(string token)
        {
            var url = Host + "/api/resource/check";
            var json = new JObject { { "token", token } };
            HttpContent content = new StringContent(json.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _httpManager.Request(url, token, content);
            return result;
        }

        public async Task<ResponseResult> GetResource(string token, int id)
        {
            var url = Host + $"/api/resource/getresource?id={id}";
            var result = await _httpManager.Request(url, token);
            return result;
        }

        public async Task<ResourcePagedResultModel> GetResourcePaged(string token, int pageIndex = 1, int pageSize = 10, string query = "", string sortby = "name", string sortbyType = "asc")
        {
            var url = Host + $"/api/resource/getresources?pageIndex={pageIndex}&pageSize={pageSize}&value={query}&sortBy={sortby} {sortbyType}";
            var reuslt = await _httpManager.Request(url, token);
            if (reuslt.Status == "0")
                return reuslt.Data == null ? new ResourcePagedResultModel() : JsonConvert.DeserializeObject<ResourcePagedResultModel>(reuslt.Data.ToString());
            return new ResourcePagedResultModel();
        }

        public CreateOrUpdateResultModel CreateResource(string token, string name, string fileName, string description)
        {
            var header = new WebHeaderCollection { $"Authorization:Bearer {token}" };
            var url = Host + $"/api/resource/create";
            var postVars = new System.Collections.Specialized.NameValueCollection
            {
                {"id", "0"},
                {"name", name},
                {"filename", fileName},
                {"description", description}
            };
            //添加值域
            var result = HttpService.HttpPost(url, header, postVars);
            return string.IsNullOrEmpty(result) ? new CreateOrUpdateResultModel() : JsonConvert.DeserializeObject<CreateOrUpdateResultModel>(result);
        }

        public CreateOrUpdateResultModel UpdateResource(string token, int id, string name, string fileName, string description)
        {
            var header = new WebHeaderCollection { $"Authorization:Bearer {token}" };
            var url = Host + $"/api/resource/udpate";
            var postVars = new System.Collections.Specialized.NameValueCollection
            {
                {"id", id.ToString()},
                {"name", name},
                {"filename", fileName},
                {"description", description}
            };
            //添加值域
            var result = HttpService.HttpPost(url, header, postVars);
            return string.IsNullOrEmpty(result) ? new CreateOrUpdateResultModel() : JsonConvert.DeserializeObject<CreateOrUpdateResultModel>(result);
        }

        public async Task<ResponseResult> UpdateUpload(string token, int id, string path)
        {
            var url = Host + "/api/resource/updateupload";
            var json = new JObject { { "id", id }, { "path", path } };
            HttpContent content = new StringContent(json.ToString());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await _httpManager.Request(url, token, content);
            return result;
        }

        #endregion

    }
}
