using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using St.Upload.ViewModel;

namespace St.Upload.Service
{
    public class FileService
    {
        /// <summary>
        /// 将文件转换成流
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <returns></returns>
        public static byte[] SetImageToByteArray(string fileName)
        {
            byte[] image = null;
            try
            {
                var fs = new FileStream(fileName, FileMode.Open);
                var fileInfo = new FileInfo(fileName);
                //fileSize = Convert.ToDecimal(fileInfo.Length / 1024).ToString("f2") + " K";
                var streamLength = (int)fs.Length;
                image = new byte[streamLength];
                fs.Read(image, 0, streamLength);
                fs.Close();
                return image;
            }
            catch
            {
                return image;
            }
        }

        public static byte[] ReadStreamFromFile(string filePath, string fileName, long start, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var startTime = DateTime.Now;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    if (start >= stream.Length) return null;
                    if (stream.Length - start < bufferSize)
                    {
                        buffer = new byte[stream.Length - start];
                        bufferSize = (int)(stream.Length - start);
                    }
                    stream.Position = start;
                    stream.Read(buffer, 0, bufferSize);
                    return buffer;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    stream.Close();
                    stream.Dispose();
                    var t2 = DateTime.Now;
                    var t = DateDiff(t2, startTime);
                }
            }
        }

        private static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        public static FileModel GetFileLength(string filePath)
        {
            var model = new FileModel();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    model.FileLength = stream.Length;
                    //model.FileData = new byte[model.FileLength];
                    //stream.Read(model.FileData, 0, (int)model.FileLength);
                    using (var hash = HashAlgorithm.Create())
                    {
                        var hb = hash.ComputeHash(stream);//哈希算法根据文本得到哈希码的字节数组 
                        model.FileHashCode = BitConverter.ToString(hb);
                    }
                    return model;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
