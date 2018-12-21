using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using St.Common;
using St.Upload.ViewModel;

namespace St.Upload.Service
{
    public class HttpService
    {
        public static string HttpPostData(Uri uri, List<UploadFile> files)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;
            var stream = new MemoryStream();
            byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            //提交文件
            if (files != null)
            {
                string fformat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                foreach (UploadFile file in files)
                {
                    string s = string.Format(fformat, file.Path, file.Filename);
                    byte[] data = Encoding.UTF8.GetBytes(s);
                    stream.Write(data, 0, data.Length);
                    stream.Write(file.Data, 0, file.Data.Length);
                    stream.Write(line, 0, line.Length);
                }
            }
            request.ContentLength = stream.Length;
            var requestStream = request.GetRequestStream();
            stream.Position = 0L;
            stream.CopyTo(requestStream);
            stream.Close();
            requestStream.Close();
            HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;
            string srcString;
            response = request.GetResponse() as HttpWebResponse;
            responseStream = response.GetResponseStream();
            reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            srcString = reader.ReadToEnd();
            var result = srcString;   //返回值赋值
            reader.Close();
            return result;
        }

        public string HttpPostDataBath(Uri uri, byte[] fileData)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = CredentialCache.DefaultCredentials;
            var stream = new MemoryStream();
            byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            //提交文件
            if (fileData != null)
            {
                string fformat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                string s = string.Format(fformat, "", "");
                byte[] data = Encoding.UTF8.GetBytes(s);
                stream.Write(data, 0, data.Length);
                stream.Write(fileData, 0, fileData.Length);
                stream.Write(line, 0, line.Length);
            }

            request.ContentLength = stream.Length;
            var requestStream = request.GetRequestStream();
            stream.Position = 0L;
            stream.CopyTo(requestStream);
            stream.Close();
            requestStream.Close();
            HttpWebResponse response;
            Stream responseStream;
            StreamReader reader;
            string srcString;
            response = request.GetResponse() as HttpWebResponse;
            responseStream = response.GetResponseStream();
            reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
            srcString = reader.ReadToEnd();
            var result = srcString;   //返回值赋值
            reader.Close();
            return result;
        }


        public string HttpPost(string url, string postDataStr)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 6000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postDataStr.Length;
                var writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                writer.Write(postDataStr);
                writer.Flush();
                var response = (HttpWebResponse)request.GetResponse();
                var encoding = response.ContentEncoding;
                if (encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 添加头信息的
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="method">post or get</param>
        /// <returns></returns>
        public static string HttpRequest(string url, WebHeaderCollection headers, string method)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Timeout = 5000;
                if (method.ToLower() == "post")
                    request.ContentType = "application/x-www-form-urlencoded";
                request.Headers = headers;
                var response = (HttpWebResponse)request.GetResponse();
                var encoding = response.ContentEncoding;
                if (encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static string HttpPost(string url, WebHeaderCollection headers, System.Collections.Specialized.NameValueCollection data, string type = "post")
        {
            var webClientObj = new WebClient { Headers = headers };
            if (type.ToLower() == "post")
                webClientObj.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var byRemoteInfo = webClientObj.UploadValues(url, type, data);
            return Encoding.Default.GetString(byRemoteInfo);
        }


        //public async Task<string> Request(string url, string accessToken, HttpContent content = null)
        //{
        //    var responseResult = new ResponseResult();
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri(url);
        //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //        HttpResponseMessage response = null;
        //        try
        //        {
        //            response = content == null
        //                ? await httpClient.GetAsync(url)
        //                : await httpClient.PostAsync(url, content);

        //        }
        //        catch (Exception ex)
        //        {

        //            return "";
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();


        //        }
        //        else
        //        {
        //            if (response.StatusCode == HttpStatusCode.Unauthorized)
        //            {
        //                Log.Logger.Debug($"【log out, because of timeout】：{response.StatusCode}");

        //                //var visualizeShellService =
        //                //    DependencyResolver.Current.GetService<IVisualizeShell>();
        //                //await visualizeShellService.Logout();
        //            }
        //        }
        //    }
        //}

    }
}
