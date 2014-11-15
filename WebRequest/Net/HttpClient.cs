using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using log4net;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using XXX;

namespace Spike.Core.Net
{
    /// <summary>
    /// 请求类型
    /// </summary>
    public enum HttpVerb
    {
        Get,
        Head,
        Post,
    }

    /// <summary>
    /// 网络异常
    /// </summary>
    public delegate void ConnectionIssue(WebException ex);

    /// <summary>
    /// Http请求客户端
    /// </summary>
    public class HttpClient
    {
        private static readonly ILog log = LogManager.GetLogger("HttpClient");

        #region 属性
        /// <summary>
        /// 请求编码
        /// 默认为utf-8
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 请求UA
        /// 默认：Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20
        /// </summary>
        public String UserAgent { get; set; }

        /// <summary>
        /// 请求Cookie
        /// </summary>
        public CookieContainer Cookie { get; set; }

        /// <summary>
        /// 连接超时（毫秒）
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 是否保持连接
        /// </summary>
        public bool KeepAlive { get; set; }

        #endregion

        /// <summary>
        /// 网络异常
        /// </summary>
        public event ConnectionIssue ConnectFailed = delegate { };


        public HttpClient()
        {
            this.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.110 Safari/537.36 CoolNovo/2.0.9.20";
            this.Encoding = Encoding.UTF8;
            this.Cookie = new CookieContainer();
            this.Timeout = 30000;
            this.KeepAlive = true;

            //100-Continue 行为
            ServicePointManager.Expect100Continue = true;
        }

        #region GET
        public void Get(string url, Action<string> successCallback)
        {
            Get(url, null, StreamToStringCallback(successCallback));
        }

        public void Get(string url, string referer, Action<String> successCallback)
        {
            Get(url, referer, StreamToStringCallback(successCallback), (webEx) => ConnectFailed(webEx));
        }

        public void Get(string url, string referer, Action<string> successCallback, Action<WebException> failCallback)
        {
            Get(url, referer, StreamToStringCallback(successCallback), failCallback);
        }

        public void Get(string url, Action<WebHeaderCollection, Stream> successCallback)
        {
            Get(url, null, successCallback);
        }

        public void Get(string url, string referer, Action<WebHeaderCollection, Stream> successCallback)
        {
            Get(url, referer, successCallback, (webEx) => ConnectFailed(webEx));
        }

        public void Get(string url, string referer, Action<WebHeaderCollection, Stream> successCallback, Action<WebException> failCallback)
        {
            MakeRequest(HttpVerb.Get, url, referer, null, successCallback, failCallback);
        }

        /// <summary>
        /// 非异步GET请求
        /// </summary>
        public string GetString(string url)
        {
            return GetString(url, null);
        }
        public string GetString(string url, string referer)
        {
            return StreamToString(GetStream(url, referer));
        }

        public string GetImg(string url)
        {
            //GetImg(url, null);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            string strFileName = @"img/"+Common.ConvertDateTimeInt(DateTime.Now).ToString() + ".jpg";
            FileStream writer = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buff = new byte[1024];
            int c = 0; //实际读取的字节数
            while ((c = reader.Read(buff, 0, buff.Length)) > 0)
            {
                writer.Write(buff, 0, c);
            }
            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();
            response.Close();
            return strFileName;
        }

        public string GetImgStr(string filePath)
        {

            //Bitmap sourceBmp = new Bitmap(filePath);

            //Color c = sourceBmp.GetPixel(60, 22); 
            
            //int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);//转换灰度的算法 

            //sourceBmp.SetPixel(60, 22, Color.FromArgb(luma, luma, luma));

            //Color c = sourcebm.GetPixel(x、y); 
            
            //if (c.R >= critical_value) 
            //    sourcebm.SetPixel(x,y,Color.FromArgb(255,255,255);
            //else


            return string.Empty;
        }

        public void GetImg(string url, string referer)
        {
            StreamToImage(GetStream(url, referer));
        }

        public Stream GetStream(string url)
        {
            return GetStream(url, null);
        }

        public Stream GetStream(string url, string referer)
        {
            return GetStream(url, referer, false);
        }

        public Stream GetStream(string url, string referer, bool isAjax)
        {
            return GetResponse(url, referer, null, isAjax).Stream;
        }

        public HttpResponse GetResponse(string url, string referer, object parameters, bool isAjax)
        {
            return MakeRequest(HttpVerb.Get, url, referer, parameters, isAjax);
        }

        #endregion

        #region POST

        public void Post(string url, object parameters, Action<string> successCallback)
        {
            Post(url, null, parameters, successCallback);
        }

        public void Post(string url, string referer, object parameters, Action<string> successCallback)
        {
            Post(url, referer, parameters, successCallback, (webEx) => ConnectFailed(webEx));
        }

        public void Post(string url, string referer, object parameters, Action<string> successCallback, Action<WebException> failCallback)
        {
            MakeRequest(HttpVerb.Post, url, referer, parameters, StreamToStringCallback(successCallback), failCallback);
        }


        public void Post(string url, object parameters, Action<WebHeaderCollection, Stream> successCallback)
        {
            Post(url, null, parameters, successCallback);
        }

        public void Post(string url, string referer, object parameters, Action<WebHeaderCollection, Stream> successCallback)
        {
            Post(url, referer, parameters, successCallback, (webEx) => ConnectFailed(webEx));
        }

        public void Post(string url, string referer, object parameters, Action<WebHeaderCollection, Stream> successCallback, Action<WebException> failCallback)
        {
            MakeRequest(HttpVerb.Post, url, referer, parameters, successCallback, failCallback);
        }


        public Stream PostStream(string url)
        {
            return PostStream(url, null, null);
        }

        public Stream PostStream(string url, object parameters)
        {
            return PostStream(url, null, parameters);
        }

        public Stream PostStream(string url, string referer, object parameters)
        {
            return PostStream(url, referer, parameters, false);
        }

        public Stream PostStream(string url, string referer, object parameters, bool isAjax)
        {
            return PostResponse(url, referer, parameters, isAjax).Stream;
        }

        public string PostString(string url)
        {
            return PostString(url, null, null);
        }

        public string PostString(string url, object parameters)
        {
            return PostString(url, null, parameters);
        }

        public string PostString(string url, string referer, object parameters)
        {
            return StreamToString(PostStream(url, referer, parameters));
        }

        public string PostString(string url, string referer, object parameters, bool isAjax)
        {
            return StreamToString(PostStream(url, referer, parameters, isAjax));
        }

        public HttpResponse PostResponse(string url, string referer, object parameters, bool isAjax)
        {
            return MakeRequest(HttpVerb.Post, url, referer, parameters, isAjax);
        }



        #endregion

        #region 方法
        private void MakeRequest(HttpVerb method, string url, string referer, object parameters, Action<WebHeaderCollection, Stream> successCallback, Action<WebException> failCallback)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("url is empty");
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));

                request.Method = method.ToString();
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.AllowAutoRedirect = false;
                request.Timeout = this.Timeout;
                request.KeepAlive = this.KeepAlive;
                request.CookieContainer = this.Cookie;
                request.UserAgent = this.UserAgent;

                if (!string.IsNullOrEmpty(referer))
                    request.Referer = referer;

                if (log.IsDebugEnabled)
                {
                    log.Info("=========================================");
                    log.InfoFormat("Method:{1}, Url:[{0}]", url, method);
                }

                if (method == HttpVerb.Post)
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.BeginGetRequestStream(new AsyncCallback((IAsyncResult callbackResult) =>
                    {
                        HttpWebRequest tmprequest = (HttpWebRequest)callbackResult.AsyncState;
                        Stream postStream = tmprequest.EndGetRequestStream(callbackResult);

                        string postbody = UrlUtils.SerializeQueryString(parameters);
                        byte[] byteArray = this.Encoding.GetBytes(postbody);
                        if (log.IsDebugEnabled)
                        {
                            log.InfoFormat("PostBody:{0}", postbody);
                        }

                        postStream.Write(byteArray, 0, byteArray.Length);
                        postStream.Flush();
                        postStream.Close();

                        tmprequest.BeginGetResponse(ProcessCallback(successCallback, failCallback), tmprequest);
                    }), request);
                }
                else if (method == HttpVerb.Get || method == HttpVerb.Head)
                {
                    request.BeginGetResponse(ProcessCallback(successCallback, failCallback), request);
                }
            }
            catch (WebException webEx)
            {
                failCallback(webEx);
            }
        }

        private Action<WebHeaderCollection, Stream> StreamToStringCallback(Action<string> successCallback)
        {
            return (WebHeaderCollection headers, Stream resultStream) =>
            {
                using (StreamReader sr = new StreamReader(resultStream))
                {
                    successCallback(sr.ReadToEnd());
                }
            };
        }

        private AsyncCallback ProcessCallback(Action<WebHeaderCollection, Stream> success, Action<WebException> fail)
        {
            return new AsyncCallback((callbackResult) =>
            {
                HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;

                try
                {
                    using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.EndGetResponse(callbackResult))
                    {
                        if (log.IsDebugEnabled)
                        {
                            log.InfoFormat("Url:{1}, Status:{0}", myResponse.StatusCode, myResponse.ResponseUri);
                            log.Info("Response Headers:");
                            foreach (string hk in myResponse.Headers.AllKeys)
                            {
                                log.InfoFormat("{0}:{1}", hk, myResponse.Headers[hk]);
                            }
                            log.Info("=========================================");
                        }

                        //保存Cookies
                        if (myResponse.Cookies.Count > 0) this.Cookie.Add(myResponse.Cookies);
                        success(myResponse.Headers, myResponse.GetResponseStream());
                    }
                }
                catch (WebException webEx)
                {
                    if (ConnectFailed != null)
                    {
                        fail(webEx);
                    }
                }
            });
        }

        /// <summary>
        /// 非异步请求
        /// </summary>
        private HttpResponse MakeRequest(HttpVerb method, string url, string referer, object parameters, bool isAjax)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("url is empty");
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString();
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.AllowAutoRedirect = false;
            request.Timeout = this.Timeout;
            request.KeepAlive = this.KeepAlive;
            request.CookieContainer = this.Cookie;
            request.UserAgent = this.UserAgent;

            if (isAjax)
                request.Headers["X-Requested-With"] = "XMLHttpRequest";
                
            if (!string.IsNullOrEmpty(referer))
                request.Referer = referer;

            if (log.IsDebugEnabled)
            {
                log.Info("=========================================");
                log.InfoFormat("Method:{1}, Url:[{0}]", url, method);
            }


            //post请求
            if (method == HttpVerb.Post)
            {
                string postbody = UrlUtils.SerializeQueryString(parameters);
                byte[] bs = Encoding.UTF8.GetBytes(postbody);

                if (log.IsDebugEnabled)
                {
                    log.InfoFormat("PostBody:{0}", postbody);
                }

                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = bs.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
            }

            //获取请求
            try
            {
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                if (log.IsDebugEnabled)
                {
                    log.InfoFormat("Url:{1}, Status:{0}", response.StatusCode, response.ResponseUri);
                    log.Info("Response Headers:");
                    foreach (string hk in response.Headers.AllKeys)
                    {
                        log.InfoFormat("{0}:{1}", hk, response.Headers[hk]);
                    }
                    log.Info("=========================================");
                }

                //保存Cookies
                if (response.Cookies.Count > 0)
                    this.Cookie.Add(response.Cookies);

                return new HttpResponse(this.Encoding, response.Headers, response.GetResponseStream(),
                    response.StatusCode);
            }
            catch (Exception exception)
            {
                return new HttpResponse(this.Encoding, null, null, HttpStatusCode.ExpectationFailed);
            }
        }

        private string StreamToString(Stream stream)
        {
            string str = "";
            if (stream != null && stream.CanRead)
            {
                StreamReader reader = new StreamReader(stream, this.Encoding);
                str = reader.ReadToEnd();
                reader.Close();

                if (log.IsDebugEnabled)
                {
                    log.InfoFormat("Response String:\r\n{0}", str);
                    log.Info("=========================================");
                }
            }
            return str;
        }

        public void StreamToImage(Stream stream)
        {
            if (stream != null && stream.CanRead)
            {
                StreamReader reader = new StreamReader(stream, this.Encoding);
                //FileStream fs2 = new FileStream("01.jpeg", FileMode.Create, FileAccess.Write, FileShare.None);  
                char[] farr = new char[1343];
                int rbuffer = 1343;

                int size = 0;
                //byte[] bytes = new byte[2000];
                while (reader.Read(farr,0,rbuffer) != 0)
                {

                    //size += farr.Length;
                    byte[] bytes = Encoding.GetBytes(farr);
                    ImageHelper.CreateImageFromBytes("01.jpeg", bytes);
                    
                    //fs2.Write(bytes, 0, rbuffer); 
                }


                reader.Close();
                //fs2.Close();
                
                //char[] chars = new char[2048];

                //reader.ReadBlock(chars, 0, 2048);
                //reader.Dispose();

                //byte[] bytes = Encoding.Default.GetBytes(chars);
                ////MemoryStream ms = new MemoryStream(bytes);
                ////Image img = Image.FromStream(ms);
                ////img.Save("01.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ////img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //FileStream fs = File.Create("01.txt");
                //StreamWriter sw = new StreamWriter(fs);

                //sw.Write(bytes);

                //sw.Dispose();
                ////fs.BeginWrite(bytes, 0, 2048, null, null);
                ////fs.Dispose();
                //try
                //{
                //    Image image = Image.FromFile("01.jpeg");
                //    image.Save("02.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

                //    image.Dispose();
                //}
                //catch (Exception ex)
                //{

                //}
                ////reader.
                ////reader.Close();

                //if (log.IsDebugEnabled)
                //{
                //    log.InfoFormat("Response String:\r\n{0}", "");
                //    log.Info("=========================================");
                //}
            }
       

            //Bitmap bt = new Bitmap(stream);
            //bt.Save("01.jpeg");
            //bt.Dispose();
            //FileStream fs = File.Create("01.jpeg");
            
           

            //Image im = Image.FromStream((Stream)stream);
            //im.Save("01.JPEG");
        }

        #endregion
    }


    static class UrlUtils
    {
        /// <summary>
        /// 序列化URL查询字符串
        /// </summary>
        public static string SerializeQueryString(object parameters)
        {
            string querystring = "";
            
            if (parameters != null)
            {
                if (parameters is string)
                {
                    querystring = (String) parameters;
                }
                else
                {
                    int i = 0;
                    PropertyInfo[] properties = parameters.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        //Uri.EscapeDataString
                        querystring += property.Name + "=" + Uri.EscapeUriString(property.GetValue(parameters, null).ToString());
                        if (++i < properties.Length) querystring += "&";
                    }
                }
            }
            return querystring;
        }
    }

    public static class ImageHelper
    {
        /// <summary>
        /// Convert Image to Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// Convert Byte[] to Image
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// Convert Byte[] to a picture and Store it in file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string CreateImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            Image image = BytesToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            File.WriteAllBytes(file, buffer);
            return file;
        }
    }
}
