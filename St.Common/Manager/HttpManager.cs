using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;

namespace St.Common.Manager
{
    public class HttpManager
    {
        public async Task<ResponseResult> Request(string url, string accessToken = null, HttpContent content = null)
        {
            var responseResult = new ResponseResult();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                if (accessToken != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = null;
                try
                {
                    response = content == null
                        ? await httpClient.GetAsync(url)
                        : await httpClient.PostAsync(url, content);

                }
                catch (Exception ex)
                {
                    responseResult.Status = "-1";
                    responseResult.Message = ex.Message;

                    return responseResult;
                }

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject(result, typeof(ResponseResult));
                    responseResult = obj as ResponseResult;

                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Log.Logger.Debug($"【log out, because of timeout】：{response.StatusCode}");
                    }
                    responseResult.Message = response.ReasonPhrase;
                    responseResult.Status = response.StatusCode.ToString();
                }
                return responseResult;
            }
        }


        public ResponseResult HttpPost(string url, string accessToken)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 6000000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
                var response = (HttpWebResponse)request.GetResponse();

                var encoding = response.ContentEncoding;
                if (encoding.Length < 1)
                    encoding = "UTF-8"; //默认编码
                var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                var result = reader.ReadToEnd();
                var obj = JsonConvert.DeserializeObject(result, typeof(ResponseResult));
                var responseResult = obj as ResponseResult;
                return responseResult;
            }
            catch (Exception ex)
            {
                return new ResponseResult();
            }
        }

    }
}
