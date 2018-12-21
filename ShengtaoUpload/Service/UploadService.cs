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
using St.Upload.ViewModel;

namespace St.Upload.Service
{
    public class UploadService
    {
        #region Field

        private static string _hostUrl;
        private const int Buffersieze = 1024 * 512;
        private string _cid;
        private readonly IApiService _apiService;
        private readonly HttpService _httpService;

        #endregion

        #region Ctor

        public UploadService()
        {
            _hostUrl = GlobalData.Instance.FileUploadUrl;
            _apiService = new ApiService();
            _httpService = new HttpService();
        }

        #endregion

        #region Method


        public ApplyUploadResult ApplyUpload(string fileName, string token)
        {
            try
            {
                var targetUrl = string.Format(_hostUrl + "/filesliceupload?method=create&filename={0}&token={1}",
                    fileName, token);
                var result = _httpService.HttpPost(targetUrl, string.Empty);
                var applyResultModel = JsonConvert.DeserializeObject<ApplyUploadResult>(result);
                if (applyResultModel.Code == "0") _cid = applyResultModel.Cid;
                return applyResultModel;
            }
            catch (Exception ex)
            {
                return new ApplyUploadResult();
            }
        }

        public Task<DoUploadResult> DoUpload(ref DoUploadPara parameter, string filePath, string fileName)
        {
            var start = Convert.ToInt64(parameter.Range.Split('-')[0]);
            var data = FileService.ReadStreamFromFile(filePath, fileName, start, Buffersieze);
            if (data == null) return Task.FromResult(new DoUploadResult() { Code = "-9999" });
            var targetUrl = string.Format(parameter.Host + "/filesliceupload?method=write&cid={0}&range={1}&token={2}",
                parameter.Cid, parameter.Range, parameter.Token);
            var result = _httpService.HttpPostDataBath(new Uri(targetUrl), data);
            var resultModel = JsonConvert.DeserializeObject<DoUploadResult>(result);
            Log.Logger.Information($"range{resultModel.Range}");
            return Task.FromResult(resultModel);
        }

        public FinishUploadResult FinishUpload(string host, string cid, string token, string creator = null,
            string context = null, string callback = null, string fileName = null)
        {
            try
            {
                var targetUrl =
                    $"{host}/filesliceupload?method=close&cid={cid}&token={token}&creator={creator}&context={context}&callback={callback}&filename={fileName}";
                var result = _httpService.HttpPost(targetUrl, string.Empty);
                var resultModel = JsonConvert.DeserializeObject<FinishUploadResult>(result);
                return resultModel;
            }
            catch (Exception ex)
            {
                return new FinishUploadResult();
            }

        }

        public async Task<bool> UpdateResource(string token, int id, string path)
        {
            var result = await _apiService.UpdateUpload(token, id, path);
            return result.Status == "0";
        }


        public void SaveUploadDataToLocal(UploadLocalDataModel model)
        {
            var path = Path.Combine(Environment.CurrentDirectory, GlobalResources.UploadLocalDataPath);
            var listText = File.ReadAllText(path, Encoding.UTF8);
            if (model == null) return;
            try
            {
                var list = new List<UploadLocalDataModel>();
                if (!string.IsNullOrEmpty(listText))
                {
                    list = JsonConvert.DeserializeObject<List<UploadLocalDataModel>>(listText);
                    var data = list.FirstOrDefault(o => o.ResourceId == model.ResourceId);
                    if (data != null)
                    {
                        data.Range = model.Range;
                        data.DateTime = DateTime.Now;
                        data.Status = model.Status;
                        data.Path = model.Path;
                        data.FilePath = model.FilePath;
                    }
                    else
                    {
                        list.Add(model);
                    }
                }
                else
                {
                    list.Add(model);
                }
                File.WriteAllText(path, JsonConvert.SerializeObject(list), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"保存上传结果错误：{ex.Message}");
            }
        }

        public void DeleteUploadDataFromLocal(int sourceId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, GlobalResources.UploadLocalDataPath);
            var listText = File.ReadAllText(path, Encoding.UTF8);
            try
            {
                var list = new List<UploadLocalDataModel>();
                if (!string.IsNullOrEmpty(listText))
                {
                    list = JsonConvert.DeserializeObject<List<UploadLocalDataModel>>(listText);
                    var ulist = list.Where(o => o.ResourceId != sourceId);
                    if (ulist.Any())
                        File.WriteAllText(path, JsonConvert.SerializeObject(ulist), Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"保存上传结果错误：{ex.Message}");
            }
        }

        public UploadLocalDataModel GetLastUploadRecord(int resourceId, string key)
        {
            var path = Path.Combine(Environment.CurrentDirectory, GlobalResources.UploadLocalDataPath);
            var listText = File.ReadAllText(path, Encoding.UTF8);
            try
            {
                var list = JsonConvert.DeserializeObject<List<UploadLocalDataModel>>(listText);
                var data = list.FirstOrDefault(o => o.Key == key && o.ResourceId == resourceId && o.Status == "0");
                return data;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"获取上传记录错误：{ex.Message}");
                return null;
            }
        }

        public UploadLocalDataModel GetLastUploadRecord(int resourceId)
        {
            var path = Path.Combine(Environment.CurrentDirectory, GlobalResources.UploadLocalDataPath);
            var listText = File.ReadAllText(path, Encoding.UTF8);
            try
            {
                var list = JsonConvert.DeserializeObject<List<UploadLocalDataModel>>(listText);
                var data = list.FirstOrDefault(o => o.ResourceId == resourceId && o.Status == "0");
                return data;
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"获取上传记录错误：{ex.Message}");
                return null;
            }
        }


        #endregion


    }
}
